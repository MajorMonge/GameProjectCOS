using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 0.1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float modifier = 1f;

        if(Input.GetKey(KeyCode.LeftShift)) {
            modifier = 1.2f;
        }

        if(Input.GetKey(KeyCode.W)) 
        {
            gameObject.transform.position += gameObject.transform.forward * 1 * speed * modifier;
        } 
        else if (Input.GetKey(KeyCode.A)) 
        {
            gameObject.transform.position += gameObject.transform.right * -1 * speed * modifier;
        }
        else if (Input.GetKey(KeyCode.D)) 
        {
            gameObject.transform.position += gameObject.transform.right * 1 *speed * modifier;
        }
        else if (Input.GetKey(KeyCode.S)) 
        {
            gameObject.transform.position += gameObject.transform.forward * -1 * speed * modifier;
        }
    }
}
