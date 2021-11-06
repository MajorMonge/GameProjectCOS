using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    public Vector3 endPos;
    public float speed = 1.0f;

    public GameObject DoorReference;
    
    public float timer = .3f;
    private bool moving = false;
    private string doorState = "closed";
    private Vector3 startPos;


    // Start is called before the first frame update
    void Start()
    {
        startPos = DoorReference.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(doorState == "open")
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    void OpenDoor() 
    {
        float dist = Vector3.Distance(DoorReference.transform.position, (endPos + startPos));
        if(dist > .1f) 
        {
            DoorReference.transform.position = Vector3.MoveTowards(DoorReference.transform.position, (endPos + startPos), speed * Time.deltaTime);
        }

        if(DoorReference.transform.position == endPos) {
            Debug.Log("Fechando a porta");
            doorState = "open";
            CloseDoor();
        }
        
    }

    void CloseDoor() 
    {
        float dist = Vector3.Distance(DoorReference.transform.position, startPos);
        if(dist > .1f)
        {
            DoorReference.transform.position = Vector3.MoveTowards(DoorReference.transform.position, startPos, speed * Time.deltaTime);
        }
        
        if(DoorReference.transform.position == startPos)
        { 
            doorState = "closed";
        }
    }

    private void OnTriggerEnter(Collider collisionObj)
    {
        Debug.Log("Interação");
        if(collisionObj.gameObject.tag == "Player") 
        {
            if(doorState == "closed")
            {
                doorState = "open";
                OpenDoor();
            }
        }
    }
}
