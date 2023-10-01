using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ContentManager : MonoBehaviour
{
    public Toggle CubeToggle;
    public GameObject TestCube1Prefab;
    public GameObject TestCube2Prefab;
    private GameObject SpawnedTestCube;
    public Camera ARCamera;

    private List<RaycastResult> raycastResults = new List<RaycastResult>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Down");

            Ray ray = ARCamera.ScreenPointToRay(Input.mousePosition);
            Debug.Log(ray);

            if (IsPointerOverUI(Input.mousePosition))
            {
                Debug.Log("Do nothing");
            }
            else
            {
                SpawnedTestCube = Instantiate(WhichCube(), ray.origin, Quaternion.identity);
                SpawnedTestCube.GetComponent<Rigidbody>().AddForce(ray.direction * 100);
            }

        }
        
    }

    public GameObject WhichCube()
    {
        if (CubeToggle.isOn)
        {
            return TestCube1Prefab;
        }
        else
        {
            return TestCube2Prefab;
        }
    }

    private bool IsPointerOverUI(Vector2 fingerPosition)
    {
        PointerEventData eventDataPosition = new PointerEventData(EventSystem.current);
        eventDataPosition.position = fingerPosition;

        EventSystem.current.RaycastAll(eventDataPosition, raycastResults);
        return raycastResults.Count > 0; // if greater than zerio, that means we hit a UI element
    }
}
