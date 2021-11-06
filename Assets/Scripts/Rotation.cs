using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float mouseScrollDelta = 0;
    public float mouseSpeed = 1.2f;
    float previousMouseScrollDelta = 0;
    // Start is called before the first frame update
    void Start()
    {
        previousMouseScrollDelta = Input.GetAxis("Mouse X");
    }

    // Update is called once per frame
    void Update()
    {
        mouseScrollDelta = Input.GetAxis("Mouse X");

        float delta = mouseScrollDelta - previousMouseScrollDelta;

        transform.Rotate(Vector3.up, delta*mouseSpeed);
    }
}
