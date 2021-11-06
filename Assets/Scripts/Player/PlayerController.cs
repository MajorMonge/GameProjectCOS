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

  [Tooltip("Player's Jump Force")]
  public float JumpForce = 20f;

  [Header("Movement")]
  [Tooltip("Default movement speed")]
  public float DefaultSpeed = 5f;
  [Tooltip("Sprinting speed multiplier")]
  public float SprintMultiplier = 1.4f;

  public Vector3 CharacterMovment;
  public float MouseRotation;
  public bool IsJumping = false;
  public bool IsSprinting = false;
  public bool IsDead = false;
  public bool IsGrounded = false;

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
  }

  void HandleGravity()
  {
    CharacterMovment.y = -GravityDownForce;

    ms_Controller.Move(CharacterMovment * 2f * Time.deltaTime);
  }


  void HandlePlayerMovment()
  {

    Vector3 worldSpaceMovment = transform.TransformVector(ms_InputHandler.GetMoveInput());

    float speedMultiplier = IsSprinting ? SprintMultiplier : 1f;

    Vector3 targetVelocity = worldSpaceMovment * SprintMultiplier * DefaultSpeed;

    ms_Controller.Move(targetVelocity * Time.deltaTime);
  }

  void HandlePlayerLook()
  {
    MouseRotation -= ms_InputHandler.GetLookInput().y * ms_InputHandler.LookSensitivity;
    transform.Rotate(0f, ms_InputHandler.GetLookInput().x * ms_InputHandler.LookSensitivity, 0f);
    PlayerCamera.transform.localRotation = Quaternion.Euler(MouseRotation, 0f, 0f);
  }


}
