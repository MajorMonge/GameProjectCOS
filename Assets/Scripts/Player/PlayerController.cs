using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(InputHandler))]
public class PlayerController : MonoBehaviour
{
  [Header("References")]
  [Tooltip("Reference to the main camera used for the player")]
  public Camera PlayerCamera;

  [Header("Gravity")]
  [Tooltip("Force applied downward when in the air")]
  public float GravityDownForce = 9.8f;
  [Tooltip("Default ground gravity")]
  public float GravityGroundForce = 0.5f;

  [Header("Health")]
  [Tooltip("Player's max health")]
  public float MaxHealth = 135f;
  [Tooltip("Player's current health")]
  public float CurrentHealth = 100f;

  [Header("Stamina and JumpForce")]
  [Tooltip("Player's max stamina")]
  public float MaxStamina = 100f;

  [Tooltip("Player's stamina")]
  public float CurrentStamina = 100f;

  [Tooltip("Player's stamina recovery time")]
  public float StaminaRecoveryTime = 10f;

  [Tooltip("Player's stamina drain rate")]
  public float StaminaDrainRate = 10f;

  [Tooltip("Player's Jump Force")]
  public float JumpForce = 20f;

  [Header("Movement")]
  [Tooltip("Default movement speed")]
  public float DefaultSpeed = 5f;
  [Tooltip("Sprinting speed multiplier")]
  public float SprintMultiplier = 3f;

  public Vector3 CharacterMovment;
  public float MouseRotation;
  public float CharacterSpeed;
  public bool IsWalking = false;
  public bool IsSprinting = false;
  bool IsRechargingStamina = false;
  /* bool IsDead = false; */
  /* bool IsGrounded = false; */

  CharacterController ms_Controller;
  InputHandler ms_InputHandler;


  void Start()
  {
    ms_Controller = GetComponent<CharacterController>();
    ms_InputHandler = GetComponent<InputHandler>();
  }

  void Update()
  {
    HandleGravity();
    HandlePlayerMovment();
    HandlePlayerLook();
    HandlePlayerStamina();
    HandlePlayerHealth();
  }

  void HandleGravity()
  {
    CharacterMovment.y = -GravityDownForce;

    CharacterSpeed = IsSprinting ? SprintMultiplier : 1f;

    ms_Controller.Move(CharacterMovment * 2f * Time.deltaTime * CharacterSpeed * DefaultSpeed);
  }


  void HandlePlayerMovment()
  {

    Vector3 worldSpaceMovment = transform.TransformVector(ms_InputHandler.GetMoveInput());
    Debug.Log(ms_InputHandler.GetMoveInput().z);
    if (ms_InputHandler.GetMoveInput() != new Vector3 (0,0,0))
    {
      IsWalking = true;
    }
    else if (ms_InputHandler.GetMoveInput() == new Vector3 (0,0,0))
    {
      IsWalking = false;
    }

    CharacterSpeed = IsSprinting ? SprintMultiplier : 1f;

    Vector3 TargetVelocity = worldSpaceMovment * CharacterSpeed * DefaultSpeed;
    Debug.Log(SprintMultiplier);
    ms_Controller.Move(TargetVelocity * Time.deltaTime);
  }

  void HandlePlayerLook()
  {
    MouseRotation -= ms_InputHandler.GetLookInput().y * ms_InputHandler.LookSensitivity;
    transform.Rotate(0f, ms_InputHandler.GetLookInput().x * ms_InputHandler.LookSensitivity, 0f);
    PlayerCamera.transform.localRotation = Quaternion.Euler(Mathf.Clamp(MouseRotation, -90, 90), 0f, 0f);
  }

  void HandlePlayerHealth()
  {

  }


  void HandlePlayerStamina()
  {
    Debug.Log("IsSprinting:" + IsSprinting);
    if (Input.GetButton("Fire3"))
    {
      if (CurrentStamina > 0 && IsRechargingStamina == false)
      {
        Debug.Log("Sprinting!");
        IsSprinting = true;
        CurrentStamina -= StaminaDrainRate * Time.deltaTime;
      }
      else
      {
        IsSprinting = false;
        StartCoroutine(RechargeStamina());
      }
    }
    else if (CurrentStamina <= 0)
    {
      Debug.Log("Recharging Stamina!");
      IsSprinting = false;
      StartCoroutine(RechargeStamina());
    }
    else
    {
      IsSprinting = false;
      Debug.Log("Not Sprinting!");
    }
  }

  IEnumerator RechargeStamina()
  {
    if (IsRechargingStamina == false)
    {
      IsRechargingStamina = true;
      yield return new WaitForSeconds(StaminaRecoveryTime);
      CurrentStamina = MaxStamina;
      Debug.Log("Stamina recharged!");
      IsRechargingStamina = false;
    }
  }

}
