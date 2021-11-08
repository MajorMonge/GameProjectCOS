using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
  public bool IsDestructable;
  public float PropHP = 100f;
  public AudioClip BreakSFX;
  public AudioSource PropAudioSource;


  // Start is called before the first frame update
  void Start()
  {
    PropAudioSource = gameObject.GetComponent<AudioSource>();
    gameObject.tag = "Prop";
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void Damage(float DamageValue)
  {
    Debug.Log("Damage! " + DamageValue);
    PropHP -= DamageValue;
    if (PropHP <= 0)
    {
      PropAudioSource.clip = BreakSFX;
      PropAudioSource.Play();
      Destroy(gameObject.GetComponent<Rigidbody>());

      foreach (MeshCollider childComponent in gameObject.GetComponents<MeshCollider>())
      {
        Destroy(childComponent);
      }


      foreach (Transform child in gameObject.transform)
      {
        Destroy(child.gameObject);
      }

      StartCoroutine(DestroyProp());
    }
  }

  IEnumerator DestroyProp()
  {
    yield return new WaitForSeconds(5);
    Destroy(gameObject);
  }
}
