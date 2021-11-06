using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentHealth = 3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int damageDone) 
    {
        currentHealth -= damageDone;

        if(currentHealth <= 0) 
        {
            gameObject.SetActive(false);
        }
    }
}
