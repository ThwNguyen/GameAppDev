using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface ICharacterController
{
    bool IsMoving { get; }
    bool IsAlive { get; }
    bool IsDefending { get; }
    bool IsSliding { get; }
    bool IsRolling { get; }
    bool IsJumping { get; }

    float CurrentMoveSpeed { get; }
    void OnMove(InputAction.CallbackContext context);
    void OnJump(InputAction.CallbackContext context);
    void OnRoll(InputAction.CallbackContext context);
    void OnDefend(InputAction.CallbackContext context);
    void Onattack(InputAction.CallbackContext context);
    void OnSlide(InputAction.CallbackContext context);
    void OnHit(float dmg, Vector2 knockBack);
}