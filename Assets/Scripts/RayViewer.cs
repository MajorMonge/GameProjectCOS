using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayViewer : MonoBehaviour
{
    public float weaponRage = 50f;
    public Camera fpsCam;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Vector3 lineOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        Debug.DrawRay(lineOrigin, fpsCam.transform.forward * weaponRage, Color.green);
    }
}
