using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Gui;
using Lean.Transition;
using Unity.XR.CoreUtils;
using UnityEngine;

public enum StageMode
{
    GPSToScale,
    ToScale,
    NotToScale
}

public class EnvironmentManager : MonoBehaviour
{
    // Switching between home and UQ Lakes coordinates
    public bool useHomeCoordinates;

    // Stage: where the environment is projected
    public StageMode DefaultStage;
    public GameObject GPSToScaleStage;
    public GameObject GPSToScaleStageAlignment;
    public GameObject ToScaleStage;
    public GameObject NotToScaleStage;
    public GameObject CurrentStage;
    public StageMode CurrentStageMode;

    // Environment: the environment with all 3D objects
    public GameObject Environment;

    // State: instance of the environment that is displayed
    public GameObject StatesParent;
    public GameObject DefaultState;
    public GameObject PotentialState;
    public GameObject CurrentState;
    public bool StateIsSaved;

    // Recording: user recording responsible for the active state
    public GameObject CurrentRecording;

    // Environment transform manipulation
    public LeanJoystick PositionJoystick;
    public LeanJoystick RotationJoystick;
    public LeanJoystick ScaleJoystick;
    public LeanJoystick HeightJoystick;
    public Camera Camera;

    private Dictionary<StageMode, GameObject> stageDictionary;

    // Start is called before the first frame update
    void Start()
    {
        stageDictionary = new()
        {
            { StageMode.GPSToScale, GPSToScaleStage },
            { StageMode.ToScale, ToScaleStage },
            { StageMode.NotToScale, NotToScaleStage }
        };
        PotentialState = DefaultState;
        UpdateStage();
        UpdateState();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        UpdateRotation();
        UpdateScale();
        UpdateHeight();
    }

    public void UpdateStage(string mode = null)
    {
        CurrentStageMode = Enum.TryParse<StageMode>(mode, out var newStageMode)
            ? newStageMode
            : DefaultStage;

        CurrentStage = stageDictionary[CurrentStageMode];
        
        Environment.transform.SetParent(CurrentStage.transform);
        Environment.transform.localPosition = new Vector3(0, 0, 0);
        Environment.transform.localRotation = Quaternion.identity;
        Environment.transform.localScale = new Vector3(1, 1, 1);

        Environment.SetActive(true);
    }

    public void UpdateState()
    {
        PotentialState ??= DefaultState;

        if (!StateIsSaved && CurrentState != null)
        {
            Destroy(CurrentState);
        }
        foreach (Transform childTranform in StatesParent.transform)
        {
            if (childTranform.gameObject.Equals(PotentialState))
            {
                CurrentState = PotentialState;
                StateIsSaved = true;
                childTranform.gameObject.SetActive(true);
            }
            else
            {
                childTranform.gameObject.SetActive(false);
            }
        }

        PotentialState = null;
    }

    public void CreateState()
    {
        CurrentState.SetActive(false);

        CurrentState = Instantiate(CurrentState, StatesParent.transform);
        CurrentState.SetActive(true);

        StateIsSaved = false;
    }

    private void UpdatePosition()
    {
        var scaledValues = PositionJoystick.ScaledValue;

        var direction =
            Quaternion.Euler(0, Camera.transform.eulerAngles.y, 0)
            * new Vector3(scaledValues.x, 0, scaledValues.y)
            * -0.2f;

        CurrentStage.transform.position += direction;
    }

    private void UpdateRotation()
    {
        var scaledValues = RotationJoystick.ScaledValue;

        CurrentStage.transform.RotateAround(Camera.transform.position, Vector3.up, scaledValues.x * -1.5f);
    }

    private void UpdateScale()
    {
        var scaledValues = ScaleJoystick.ScaledValue;

        CurrentStage.transform.localScale *= 1f + scaledValues.y * 0.1f;
    }

    private void UpdateHeight()
    {
        var scaledValues = HeightJoystick.ScaledValue;

        var height =
            new Vector3(0, scaledValues.y, 0)
            * -0.2f;

        CurrentStage.transform.position += height;
    }

    public void UseHomeCoordinates(bool useHomeCoordinates)
    {
        this.useHomeCoordinates = useHomeCoordinates;
        GPSToScaleStage.GetComponent<HomeOrUQLakes>().UpdatePlaceAtLocation();
        GPSToScaleStageAlignment.GetComponent<HomeOrUQLakes>().UpdatePlaceAtLocation();
    }
}
