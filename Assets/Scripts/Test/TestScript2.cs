using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript2 : MonoBehaviour
{

    public float speed = 0.4f;
    public float rotation_damping = 4f;
    public Transform TestCube1;

    // Start is called before the first frame update
    void Start()
    {
        // Only works well with one TestCube1
        // TestCube1 = GameObject.FindGameObjectWithTag("TestCube1").GetComponent<Transform>();

        GameObject[] TestCube1s = GameObject.FindGameObjectsWithTag("TestCube1");
        int ChosenTestCube1 = Random.Range(0, TestCube1s.Length);

        TestCube1 = TestCube1s[ChosenTestCube1].GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotation of TestCube2 to camera
        var rotation = Quaternion.LookRotation(TestCube1.transform.position - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * rotation_damping);

        // TestCube2 will follow TestCube1
        float step = speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, TestCube1.position, step);
    }
}
