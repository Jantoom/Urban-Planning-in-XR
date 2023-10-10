using System.Collections;
using System.Collections.Generic;
using ARLocation;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public StateManager StateManager;
    public Transform ParentEnvironment;
    public UserRecording userRecordingPrefab1;
    public UserRecording userRecordingPrefab2;

    public void SpawnRecording()
    {
        var cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;

        var spawnPosition = cameraTransform.position + cameraTransform.rotation * Vector3.forward * 2.5f;

        var prefab = Random.Range(0.0f, 1.0f) > 0.5f ? userRecordingPrefab1 : userRecordingPrefab2;

        var instance = Instantiate(prefab, ParentEnvironment);

        instance.transform.SetPositionAndRotation(
            new Vector3(spawnPosition.x, 0f, spawnPosition.z),
            Quaternion.identity
        );

        var userRecording = instance.GetComponent<UserRecording>();
        userRecording.TextRecording = StateManager.SpeechManager.uiText.text;

        Vector3 difference = cameraTransform.position - instance.transform.position;
        float rotationY = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
        instance.transform.rotation = Quaternion.Euler(0.0f, rotationY, 0.0f);
    }
}
