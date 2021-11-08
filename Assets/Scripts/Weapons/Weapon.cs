using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

  [Header("Ammo")]
  [Tooltip("Maximum Weapon Ammo")]
  public float MaxAmmo = 100f;
  public float CurrentAmmo = 60f;

  /* [Header("Clip/Magazine Size")]
  public bool UseClip = true;
  public float ClipSize = 30f;
  public float CurrentClipAmmo = 30f; */

  [Header("Properties")]
  public bool HasFixedDamage = true;
  public int WeaponFixedDamage = 5;
  public float WeaponMinDamage = 5f;
  public float WeaponMaxDamage = 10f;
  public float ReloadTime = 5f;
  public bool ReloadLock = true;
  public float FireRate = .15f;
  public float WeaponRange = 50f;
  public float Knockback = 100f;
  private WaitForSeconds AttackDuration = new WaitForSeconds(.07f);

  [Header("Audio")]
  public AudioClip AttackSFX;
  public AudioClip RefillSFX;

  [Header("Object References")]
  public Camera CharacterCamera;
  public Transform CharacterGunPoint;
  public GameObject GunViewPrefab;
  float IsReloading;
  private float NextFire;
  private InputHandler ms_InputHandler;
  private PlayerController ms_PlayerController;
  private Animator GunViewPrefabAnimator;
  private AudioSource GunAudioSource;
  // Start is called before the first frame update
  void Start()
  {
    Debug.Log("Initializing Weapon!");

    if (CharacterGunPoint == null)
      CharacterGunPoint = GameObject.FindWithTag("CharacterGunPoint").transform;
    if (CharacterCamera == null)
      CharacterCamera = GameObject.FindWithTag("CharacterCamera").GetComponent<Camera>();
    GunAudioSource = gameObject.GetComponent<AudioSource>();
    Debug.Log(CharacterGunPoint);
    Debug.Log(CharacterCamera);


    ms_InputHandler = GameObject.FindWithTag("Player").GetComponent<InputHandler>();
    ms_PlayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
  }

  // Update is called once per frame
  void Update()
  {
    GunViewPrefabAnimator = GameObject.FindWithTag("ActiveWeaponCanvas").GetComponent<Animator>();
    HandleWeapon();
  }

  public void HandleWeapon()
  {
    if (Input.GetButtonDown("Fire1"))
    {
      Attack();
    }

    if (ms_PlayerController.IsWalking)
    {
      GunViewPrefabAnimator.SetBool("IsWalking", true);

      if (ms_PlayerController.IsSprinting)
      {
        GunViewPrefabAnimator.SetBool("IsSprinting", true);
      }
      else
      {
        GunViewPrefabAnimator.SetBool("IsSprinting", false);

      }
    }
    else
    {
      GunViewPrefabAnimator.SetBool("IsWalking", false);
    }


  }

  public void Attack()
  {
    if (Time.time > NextFire && CurrentAmmo > 0)
    {
      GunAudioSource.clip = AttackSFX;
      GunAudioSource.Play();

      GunViewPrefabAnimator.ResetTrigger("Attack");
      GunViewPrefabAnimator.SetTrigger("Attack");

      NextFire = Time.time + FireRate;

      Vector3 RayOrigin = CharacterCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

      RaycastHit Hit;

      if (Physics.Raycast(RayOrigin, CharacterCamera.transform.forward, out Hit, WeaponRange))
      {
        if (Hit.rigidbody != null)
        {
          GameObject GameProp = Hit.collider.gameObject;
          Debug.Log(GameProp.tag);
          if (GameProp.tag == "Prop")
          {

            Debug.Log("Attacked Prop");
            Prop ObjectProp = GameProp.GetComponent<Prop>();
            if (ObjectProp.IsDestructable)
            {
              Debug.Log("Execute Damage function");
              ObjectProp.Damage(Random.Range(WeaponMinDamage, WeaponMaxDamage));
            }

          }

          Hit.rigidbody.AddForce(-Hit.normal * Knockback);
        }
      }

      Debug.Log(CurrentAmmo);

      if (CurrentAmmo > 0)
      {
        CurrentAmmo -= 1;
      }
    }
  }

  public void Refill(float AmmoFillQuantity)
  {
    float CurrentAmmoFill = 0f;

    while (CurrentAmmoFill < MaxAmmo && CurrentAmmoFill < AmmoFillQuantity)
    {
      CurrentAmmoFill++;
    }
  }
}
