using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBase : MonoBehaviour
{
    protected Rigidbody2D rigi;
    protected Vector2 moveInput;
    protected Animator animator;
    public TouchingDirections touchingDirections;
    protected Damageable damageable;
    protected CountDownTime countdown;

    public float walkSpeed = 5f;
    public float jumpImpulse = 10f;
    public float rollSpeed = 10f;
    public float slideSpeed = 10f;
    public float airSpeed = 8f;

    protected bool _isdefending = false;
    protected bool _isMoving = false;
    protected bool _isFacingRight = true;
    protected bool _isSlide = false;
    protected bool _isRolling = false;

    protected float slideDuration = 0.4f;
    protected float rollDuration = 0.2f;

    protected virtual void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
        rigi = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = this.GetComponent<Damageable>();
        countdown = this.GetComponent<CountDownTime>();
    }

}