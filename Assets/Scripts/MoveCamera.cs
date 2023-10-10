using System.Collections;
using System.Collections.Generic;
using Lean.Gui;
using Lean.Transition;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public LeanJoystick Joystick;
    public GameObject Camera;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        var scaledValues = Joystick.ScaledValue;
        Camera.transform.localPosition = Camera.transform.localPosition +
            Camera.transform.rotation * new Vector3(scaledValues.x, 0, scaledValues.y) * 0.3f;
    }
}
