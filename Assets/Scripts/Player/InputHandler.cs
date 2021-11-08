using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

  [Tooltip("Locks and hide the cursor to the game screen")]
  public bool CursorIsLocked = true;

  [Header("Camera sensitivity")]
  [Tooltip("Sensitivity multiplier for moving the camera around")]
  public float LookSensitivity = 1f;

  PlayerController ms_PlayerController;

  void Start()
  {
    ms_PlayerController = GetComponent<PlayerController>();
  }

  void Update()
  {

    if (CursorIsLocked)
    {
      Cursor.visible = false;
      Cursor.lockState = CursorLockMode.Locked;
    }
    else
    {
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;
    }

  }

  public bool GetFireAction()
  {
    if (Input.GetButtonDown("Fire"))
      return true;
    else
      return false;
  }

  public Vector3 GetMoveInput()
  {
    if (CursorIsLocked)
    {
      Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

      // limita o entrada dos eixos para no máximo 1 para evitar que movimentos em diagonal excedam a quantidade desejada
      move = Vector3.ClampMagnitude(move, 1);

      return move;
    }

    return Vector3.zero;
  }

  public Vector2 GetLookInput()
  {
    if (CursorIsLocked)
    {
      Vector2 look = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

      // limita o entrada dos eixos para no máximo 1 para evitar que movimentos em diagonal excedam a quantidade desejada
      look = Vector2.ClampMagnitude(look, 1);

      return look;
    }

    return Vector2.zero;
  }
}
