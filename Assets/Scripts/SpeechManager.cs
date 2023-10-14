using System.Collections;
using System.Collections.Generic;
using BrainCheck;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpeechManager : MonoBehaviour
{
    public Text DebugSpeechRecognitionText;
    public UnityEvent PermissionGranted;
    public UnityEvent PermissionNotGranted;
    public UnityEvent SpeechRecognitionFinished;
    public UnityEvent TextToSpeechConversionStarted;
    public UnityEvent TextToSpeechConversionCompleted;
    public UnityEvent SpeechRecognitionUpdated;

    private UIManager uIManager;

    void Start()
    {
        uIManager = GetComponent<UIManager>();
        SpeechRecognitionBridge.setUnityGameObjectNameAndMethodName(
            gameObject.name,
            "SpeechCallback"
        );
        SpeechRecognitionBridge.SetupPlugin();
        SpeechRecognitionBridge.checkMicPermission();
    }

    public void SpeechCallback(string message)
    {
        if (message.Equals("PermissionGranted"))
        {
            PermissionGranted?.Invoke();
        }
        else if (message.Equals("PermissionNotGranted"))
        {
            PermissionNotGranted?.Invoke();
            SpeechRecognitionBridge.requestMicPermission();
        }
        else if (message.Equals("SpeechRecognitionFinished"))
        {
            SpeechRecognitionFinished?.Invoke();
        }
        else if (message.Equals("TextToSpeechConversionStarted"))
        {
            TextToSpeechConversionStarted?.Invoke();
        }
        else if (message.Equals("TextToSpeechConversionCompleted"))
        {
            TextToSpeechConversionCompleted?.Invoke();
        }
        else
        {
            SpeechRecognitionUpdated?.Invoke();
            uIManager.RecordingText.text = message;
            return;
        }
        DebugSpeechRecognitionText.text = message;
    }

    public void StartSpeechToText()
    {
        SpeechRecognitionBridge.speechToTextInHidenModeWithBeepSound();
    }

    public void StartTextToSpeech()
    {
        SpeechRecognitionBridge.textToSpeech(uIManager.RecordingText.text, 0);
    }

    public void UnmuteSpeakers()
    {
        SpeechRecognitionBridge.unmuteSpeakers();
    }

    public void RequestMicPermission()
    {
        SpeechRecognitionBridge.requestMicPermission();
    }
}
