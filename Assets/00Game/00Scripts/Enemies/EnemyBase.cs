using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyBase : Singleton<EnemyBase>,IAttackable
{
    protected Rigidbody2D rigi;
    protected TouchingDirections touchingDirection;
    protected Animator animator;
    protected Damageable damageable;
    [SerializeField]
    protected DetectionRange detectionRange;
    [SerializeField]
    protected DetectionZone attackZone;
    [SerializeField]
    protected DetectionZone cliffDetection;

    [SerializeField]
    protected float distanceTG;

    public float walkAcceleration = 3f;
    public float maxSpeed = 3f;
    public float walkStopRate = 0.05f;
    private Vector2 walkDirectionVector = Vector2.right;
   
    private float shootTimer = 0f;
    [SerializeField]
     protected float shootCooldown = 1f;
    [SerializeField]
    private bool _canShoot = true;
    private bool _hasTarget;

    [Header("-----HPBAR-----")]
    [SerializeField]
      protected HealthBarE HPBar;



    private WalkalbeDirection _walkDirection;
    public enum WalkalbeDirection
    {
        Right,
        Left
    }

    public WalkalbeDirection WalkDirection
    {
        get
        { 
            return _walkDirection;
        }
        set
        {
            if (_walkDirection != value)
            {
                //Direction flipped
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkalbeDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkalbeDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }

    public bool CanShoot
    {
        get 
        {
            return _canShoot;
        }
        set 
        { 
            _canShoot = value; 
        }
    }

    public bool HasTarget
    {
        get
        { 
            return _hasTarget;
        }
        protected set 
        { 
            _hasTarget = value; 
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }
    private bool _freeze = false;
    public bool Freeze
    {
        get
        {
            return _freeze;
        }
        protected set
        {
            _freeze = value;
            if (value)
            {
                animator.SetBool(AnimationStrings.freeze, value);
            }
          
        }
    }

    public bool CanMove
    {
        get { return animator.GetBool(AnimationStrings.canMove); }
    }

    public float AttackCooldown
    {
        get { return animator.GetFloat(AnimationStrings.attackCooldown); }
        private set { animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0)); }
    }

    protected virtual void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
    }

    protected virtual void Start()
    {
       
        detectionRange = GetComponentInChildren<DetectionRange>();
        animator = GetComponentInChildren<Animator>();
    }

    protected virtual void FixedUpdate()
    {
        if (touchingDirection.IsOnWall && touchingDirection.IsGrounded && !HasTarget && !detectionRange.HasTarget)
        {
            FlipDirection();
        }
        if (!damageable.LockVelocity)
        {
            if (CanMove && detectionRange.HasTarget)
            {    

                if (CanShoot && GameManager.Instant.Player.touchingDirections.IsGrounded&&!Freeze)
                {
                    animator.SetTrigger(AnimationStrings.shootTrigger);
                    CanShoot = false;
                }
                if (distanceTG < Vector3.Distance(detectionRange.playerPosition, transform.position)&& !cliffDetection.canFlip)
                {
                    Vector3 targetPosition = detectionRange.playerPosition;
                    Vector2 directionToTarget = (targetPosition - transform.position).normalized;

                    WalkDirection = (directionToTarget.x > 0) ? WalkalbeDirection.Right : WalkalbeDirection.Left;

                    rigi.velocity = new Vector2(maxSpeed * directionToTarget.x, rigi.velocity.y);
                }else
                {
                    rigi.velocity = Vector3.zero;
                }
            }
            else if (CanMove && !HasTarget)
            {
                rigi.velocity = new Vector2(maxSpeed * walkDirectionVector.x, rigi.velocity.y);
            }
            else
            {
                rigi.velocity = new Vector2(Mathf.Lerp(rigi.velocity.x, 0, walkStopRate), rigi.velocity.y);
            }
        }
    }

    protected virtual void Update()
    {
        Freeze = !GameManager.Instant.Player.IsAlive;
      
      if (!Freeze)
        {
            HasTarget = attackZone.detectionColliders.Count > 0;
        }
        else
        {
            HasTarget=false;
        }
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }

        if (!CanShoot)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootCooldown)
            {
                CanShoot = true;
                shootTimer = 0f;
            }
        }
    }

    protected void FlipDirection()
    {
        if (WalkDirection == WalkalbeDirection.Right)
        {
            WalkDirection = WalkalbeDirection.Left;
        }
        else if (WalkDirection == WalkalbeDirection.Left)
        {
            WalkDirection = WalkalbeDirection.Right;
        }
    }

    public abstract void OnHit(float dmg, Vector2 knockBack);
    public abstract void OnCliffDetection();
}
