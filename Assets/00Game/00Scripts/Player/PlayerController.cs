using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class PlayerController : CharacterControllerBase, ICharacterController
{
    public bool isOnPlatform;
    public Rigidbody2D platformrg;
    public bool IsDefending
    {
        get
        {
            return _isdefending;
        }
        private set
        {
            _isdefending = value;
            animator.SetBool(AnimationStrings.defend, value);

        }
    }

    [SerializeField]
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }
    [SerializeField]
   
    public bool IsSliding
    {
        get
        {
            return _isSlide;
        }
        private set
        {
            _isSlide = value;
            animator.SetBool(AnimationStrings.slide, value);

        }
    }
    [SerializeField]
   
    public bool IsRolling
    {
        get
        {
            return _isRolling;
        }
        private set
        {
            _isRolling = value;
            animator.SetBool(AnimationStrings.roll, value);

        }
    }
    private IEnumerator EndSlide()
    {
        yield return new WaitForSeconds(slideDuration);
        IsSliding = false;
       
    }
    private IEnumerator EndRoll()
    {
        yield return new WaitForSeconds(rollDuration);
        IsRolling = false;

    }
    public float CurrentMoveSpeed
    {
        get
        {

            if (CanMove)
            {

                if (IsMoving && !touchingDirections.IsOnWall|| IsSliding)
                {
                    if (touchingDirections.IsGrounded)
                    {

                        return walkSpeed;

                    }

                    else return airSpeed;
                }
                else
                {  //Idle is 0
                    return 0;
                }
            }
            else
            {//Movement Locked
                return 0;
            }
        }

    }
    public float jumpCounter = 0;
    public float jumpTime = 1;
    [SerializeField]
    private bool _isJumping = false;
    public bool IsJumping
    {
        get
        {
            return _isJumping;
        }
        private set
        {
            _isJumping = value;
          

        }
    }


    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }
    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
       
    }



    Vector2 vecGravity;
    [SerializeField] float fallMultiplier;
    [SerializeField] float jumpMultiplier;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start(); 
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsAlive)
        {
            CharacterEvents.lost.Invoke();
            rigi.gravityScale = 5;
        }
        if (rigi.velocity.y < 0)
        {
           
            rigi.velocity -= vecGravity* fallMultiplier*Time.deltaTime;
        }
        if(rigi.velocity.y > 0 && IsJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime) IsJumping = false;

            float t = jumpCounter / jumpTime;
            float curruntJumpM = jumpMultiplier;
            if (t > 0.5f)
            {
                curruntJumpM = jumpMultiplier * (1 - t);
            }
            rigi.velocity += vecGravity * curruntJumpM * Time.deltaTime;
        }
       


    }
    private void FixedUpdate()
    {
        if (!damageable.LockVelocity )
        { 
            if(IsRolling||IsSliding)
            {
                float slideDirection = IsFacingRight ? 1f : -1f;
                rigi.velocity = new Vector2(slideDirection * slideSpeed, rigi.velocity.y);
            }
            else if (isOnPlatform)
            {
                rigi.velocity = new Vector2(moveInput.x * CurrentMoveSpeed+platformrg.velocity.x, rigi.velocity.y);
            }
            else 
            {

                rigi.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rigi.velocity.y);
            }

        }
        animator.SetFloat(AnimationStrings.yVelocity, rigi.velocity.y);
      

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive&&!IsDefending&&CanMove)
        {
            SetfacingDirection(moveInput);
            IsMoving = moveInput != Vector2.zero;
        }
        else
        {
            IsMoving = false;
        }

    }

    private void SetfacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;

        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    { //TODO check if alive as well
        if (context.started && touchingDirections.IsGrounded&&CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rigi.velocity = new Vector2(rigi.velocity.x, jumpImpulse);
            IsJumping = true;
            rigi.gravityScale = 1f;
        }
        if (context.canceled)
        {
            IsJumping = false;
            jumpCounter = 0;
        }


    }
  
     public void OnRoll(InputAction.CallbackContext context)
        {
         
        if (context.started && IsAlive && !IsDefending && CanMove)
        {
            if (touchingDirections.IsGrounded && !touchingDirections.IsOnWall)
            {
              
                IsRolling = true;
                StartCoroutine(EndRoll());
              
            }
        }
        else if (context.canceled && IsSliding)
        {
            // Stop sliding if the input is released
            IsRolling = false;
            // Apply a small counter-force to stop sliding immediately
            rigi.velocity = new Vector2(0f, rigi.velocity.y);
        }
    }

    
    public void OnDefend(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded)
        {

            damageable.Armor = 100;
            IsDefending = true;
        }
        else if (context.canceled)
        {
            damageable.Armor = 0;
            IsDefending = false;
        }
            
    }
    public void OnFireBow(InputAction.CallbackContext context)
    {
      
        if (context.started&&countdown.CanFireBow&&touchingDirections.IsGrounded)
        {

            animator.SetTrigger(AnimationStrings.rangedAttackTrigger);
            animator.SetTrigger(AnimationStrings.fireBowTrigger);
            countdown.CanFireBow = false;
        }
    }
    public void OnRainOfArrows(InputAction.CallbackContext context)
    {
       
        if (context.started&&countdown.CanRainOfArrows && touchingDirections.IsGrounded&&rainPoint.Instant.HasTarget)
        {

            animator.SetTrigger(AnimationStrings.rangedAttackTrigger);
            animator.SetTrigger(AnimationStrings.rainOfArrowsTrigger);
            countdown.CanRainOfArrows=false;

        }
    }
    public void OnSPLaze(InputAction.CallbackContext context)
    {
       
        if (context.started && countdown.CanSPLaze && touchingDirections.IsGrounded)
        {

            animator.SetTrigger(AnimationStrings.rangedAttackTrigger);
            animator.SetTrigger(AnimationStrings.spLazeTrigger);
            countdown.CanSPLaze=false;
        }
    }

    public void Onattack(InputAction.CallbackContext context)
    {
        if (context.started&&countdown.CanAttack&&touchingDirections.IsGrounded)
        {

            animator.SetTrigger(AnimationStrings.attackTrigger);
            countdown.CanAttack=false;
        }else if (context.started && countdown.CanAirAttack)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
            countdown.CanAirAttack = false;
        }
    }
    public void OnSlide(InputAction.CallbackContext context)
    {
        if (context.started && IsAlive && !IsDefending && CanMove)
        {
            if (touchingDirections.IsGrounded&&!touchingDirections.IsOnWall)
            {
               
                IsSliding = true;
                StartCoroutine(EndSlide());
               
            }
        }
        else if (context.canceled && IsSliding)
        {
            // Stop sliding if the input is released
            IsSliding = false;
            // Apply a small counter-force to stop sliding immediately
            rigi.velocity = new Vector2(0f, rigi.velocity.y);
        }
    }

    public void OnHit(float dmg, Vector2 knockBack)
    {
        // LockVelocity = true;
        rigi.velocity = new Vector2(knockBack.x, rigi.velocity.y + knockBack.y);
    }

}