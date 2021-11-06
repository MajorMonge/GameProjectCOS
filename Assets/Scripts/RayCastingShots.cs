using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastingShots : MonoBehaviour
{
    // Variáveis públicas.
    public int gunDamage = 5;
    public float fireRate = .15f;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Transform gunEnd;

    // Variáveis privadas.
    public Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    //private AudioSource gunAudio;
    public LineRenderer laserLine;
    private float nextFire;

    // Start is called before the first frame update
    void Awake() 
    {
        gunDamage = Random.Range(5,15);
    }
    void Start()
    {
        //gunAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if((Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1")) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            //StartCoroutine(ShotEffect());
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            RaycastHit hit;

            laserLine.enabled = true;
            laserLine.SetPosition(0, fpsCam.transform.position);
            laserLine.SetPosition(1, fpsCam.transform.position + (fpsCam.transform.forward * weaponRange));


            if(Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange)) 
            {
                laserLine.SetPosition(1, hit.point);

                Enemy health = hit.collider.GetComponent<Enemy>();
                Debug.Log(health);

                if (health != null) 
                {
                    health.Damage (gunDamage);
                }

                if (hit.rigidbody != null) 
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            } else 
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
        }

    }

    private IEnumerator ShotEffect() 
    {
        //gunAudio.Play();
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}
