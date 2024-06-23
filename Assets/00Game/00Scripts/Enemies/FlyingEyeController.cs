using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeController : MonoBehaviour
{
    public float flightSpeed = 3f;
    public float waypointReachedDistance = 0.1f;
    public DetectionZone biteDetectionZone;
    public List<Transform> wayPoints;
    public Collider2D deathCollider;

    [Header("-----HPBAR-----")]
    [SerializeField]
    protected HealthBarE HPBar;

    DetectionRange detectionRange;


    Damageable damageable;
    Animator animator;
    Rigidbody2D rigi;


    Transform nextWayPoint;
    int wayPointNum = 0;

  
    private float shootTimer = 0f;
    [SerializeField]
    float airAttackCooldown = 1f;

    [SerializeField]
    private bool _canAirAttack = true;
    public bool CanAirAttack
    {
        get
        {
            return _canAirAttack;
        }
        set
        {
            _canAirAttack = value;
        }
    }
  

    private bool _hasTarget;
    public bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }

    }

    private void Awake()
    {
      
        rigi = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
       
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        detectionRange = GetComponentInChildren<DetectionRange>();
        nextWayPoint = wayPoints[wayPointNum];
        HPBar.SetHealth(damageable.Health, damageable.MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {

        HPBar.SetHealth(damageable.Health, damageable.MaxHealth);

        HasTarget = biteDetectionZone.detectionColliders.Count > 0;
        if (!CanAirAttack)
        {
             
               shootTimer += Time.deltaTime;
            if (shootTimer >= airAttackCooldown)
            {
                CanAirAttack = true;
                shootTimer = 0f;
            }
        }
        
    
    }
    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                rigi.velocity = Vector2.zero;
            }
        }


    }
    public void Flight()
    {
        if (detectionRange.HasTarget)
        {
            if (CanAirAttack)
            {
                animator.SetTrigger(AnimationStrings.attackTrigger);
                CanAirAttack = false;
            }
           
            Vector2 targetPosition = detectionRange.playerPosition; 

            Vector2 directionToTarget = (targetPosition - (Vector2)transform.position).normalized;
            rigi.velocity = directionToTarget * flightSpeed;

          
            UpdateDirection();
        }
        else
        {
            
            Vector2 directionToWaypoint = (nextWayPoint.position - transform.position).normalized;

            
            float distance = Vector2.Distance(nextWayPoint.position, transform.position);

            rigi.velocity = directionToWaypoint * flightSpeed;

            
            UpdateDirection();

            if (distance <= waypointReachedDistance)
            {
                wayPointNum++;
                if (wayPointNum >= wayPoints.Count)
                {
                    wayPointNum = 0;
                }
                nextWayPoint = wayPoints[wayPointNum];
            }
        }
    }


    private void UpdateDirection()
    {
        Vector3 localScale = transform.localScale;

        //Facing the right
        if (transform.localScale.x > 0f)
        {
            if (rigi.velocity.x < 0f)
            {

                transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
            }

        }   //Facing the left
        else
        {
            if (rigi.velocity.x > 0f)
            {

                transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
            }
        }
    }

    public void OnDeath()
    {

        rigi.gravityScale = 1f;
        rigi.velocity = new Vector2(0, rigi.velocity.y);
        deathCollider.enabled = true;

    }
}
