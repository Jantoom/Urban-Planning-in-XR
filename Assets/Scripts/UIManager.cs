using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject RecordButton;
    public GameObject PlayButton;
    public GameObject SaveButton;
    public GameObject RecordingText;

    // Start is called before the first frame update
    void Start() { }

    public void SetBaseUI()
    {
        RecordButton.SetActive(true);
        PlayButton.SetActive(false);
        SaveButton.SetActive(false);
        RecordingText.SetActive(false);
    }

    // Update is called once per frame
    void Update() { }
}
