using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UserRecording : MonoBehaviour
{
    public UnityEvent clickEvent;
    public string TextRecording;
    Quaternion destinationRotation;
    float speed = 0.05f;
    float timeCount = 0.0f;
    float timeOfLook = 0.0f;

    void OnMouseUp()
    {
        Debug.Log("Up");
        clickEvent?.Invoke();
    }

    public void LookAtCamera()
    {
        var camera = GameObject.FindGameObjectWithTag("MainCamera");

        Vector3 difference = camera.transform.position - transform.position;
        float rotationY = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
        destinationRotation = Quaternion.Euler(0.0f, rotationY, 0.0f);
        timeOfLook = Time.time;

        GameObject
            .FindGameObjectWithTag("StateManager")
            .GetComponent<StateManager>()
            .SpeechManager.uiText.text = TextRecording;
    }

    void Update()
    {
        if (timeOfLook + 1f / speed > Time.time)
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
