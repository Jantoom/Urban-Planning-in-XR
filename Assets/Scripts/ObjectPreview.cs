using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPreview : MonoBehaviour
{
    public SpawnManager spawnManager;
    public GameObject Prefab;
    public Texture2D Preview;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject
            .FindGameObjectWithTag("StateManager")
            .GetComponent<SpawnManager>();
        gameObject.GetComponent<RawImage>().texture = Preview;
    }

    public void ChooseObject()
    {
        spawnManager.SpawnProp(Prefab);
    }
}
