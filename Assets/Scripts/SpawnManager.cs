using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject RecordingsParent;
    public GameObject userRecordingPrefab1;
    public GameObject userRecordingPrefab2;

    private EnvironmentManager environmentManager;
    private UIManager uIManager;
    private Camera mainCamera;

    void Start()
    {
        environmentManager = GetComponent<EnvironmentManager>();
        uIManager = GetComponent<UIManager>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void SpawnRecording()
    {
        if (!environmentManager.StateIsSaved)
        {
            environmentManager.StateIsSaved = true;
        }

        var recording = SpawnObject(
            Random.Range(0.0f, 1.0f) > 0.5f ? userRecordingPrefab1 : userRecordingPrefab2,
            RecordingsParent
        );

        recording.GetComponent<UserRecording>().RecordingState = environmentManager.CurrentState;
        recording.GetComponent<UserRecording>().TextRecording = uIManager.RecordingText.text;
    }

    public void SpawnProp(GameObject prefab)
    {
        if (environmentManager.StateIsSaved)
        {
            environmentManager.CreateState();
        }

        var prop = SpawnObject(prefab, environmentManager.CurrentState);

        var leanSelectableByFinger = prop.AddComponent<LeanSelectableByFinger>();
        var leanDragTranslate = prop.AddComponent<LeanDragTranslate>();
        var leanTwistRotateAxis = prop.AddComponent<LeanTwistRotateAxis>();
        leanDragTranslate.Use.RequiredSelectable = leanSelectableByFinger;
        leanTwistRotateAxis.Use.RequiredSelectable = leanSelectableByFinger;

        var outline = prop.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 5f;
        outline.enabled = false;

        leanSelectableByFinger.OnSelected.AddListener((_) => outline.enabled = true);
        leanSelectableByFinger.OnDeselected.AddListener((_) => outline.enabled = false);
    }

    private GameObject SpawnObject(GameObject prefab, GameObject parent)
    {
        var instance = Instantiate(prefab, parent.transform);

        var spawnPosition =
            mainCamera.transform.position
            + mainCamera.transform.rotation
                * Vector3.forward
                * (environmentManager.CurrentStageMode.Equals(StageMode.NotToScale) ? 0.25f : 2.5f);

        var difference = mainCamera.transform.position - spawnPosition;
        var spawnRotation = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;

        instance.transform.SetPositionAndRotation(
            new Vector3(spawnPosition.x, instance.transform.position.y, spawnPosition.z),
            Quaternion.Euler(0.0f, spawnRotation, 0.0f)
        );

        return instance;
    }
}
