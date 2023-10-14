using System.Collections;
using System.Collections.Generic;
using Lean.Common;
using Lean.Touch;
using UnityEngine;
using UnityEngine.Events;

public enum UserRecordingVote {
    NoVote, Upvote, Downvote
}

public class UserRecording : MonoBehaviour
{
    public GameObject RecordingState;
    public string TextRecording;
    public int TextVotes = 0;
    public UserRecordingVote vote = UserRecordingVote.NoVote;
    Quaternion destinationRotation;
    float speed = 0.1f;
    float timeCount = 0.0f;
    float timeOfLook = 0.0f;

    private EnvironmentManager environmentManager;
    private UIManager uIManager;
    private Camera mainCamera;

    void Start()
    {
        environmentManager = GameObject
            .FindGameObjectWithTag("StateManager")
            .GetComponent<EnvironmentManager>();
        uIManager = GameObject.FindGameObjectWithTag("StateManager").GetComponent<UIManager>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void LookAtCamera()
    {
        Vector3 difference = mainCamera.transform.position - transform.position;
        float rotationY = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
        destinationRotation = Quaternion.Euler(0.0f, rotationY, 0.0f);
        timeOfLook = Time.time;
        timeCount = 0f;

        environmentManager.PotentialState = RecordingState;
        uIManager.UpdateUserRecording(this);
    }

    void Update()
    {
        if (timeOfLook + 0.5f / speed > Time.time)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                destinationRotation,
                timeCount * speed
            );
        }

        timeCount += Time.deltaTime;
    }
}
