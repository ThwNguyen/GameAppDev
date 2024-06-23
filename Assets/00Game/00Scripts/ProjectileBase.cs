using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class ProjectileBase : Singleton<ProjectileBase>
{
    public float damage = 10;
    public Vector2 moveSpeed = new Vector2(3f, 0);
    public Vector2 knockBack = Vector2.zero;
    [SerializeField]
    protected float _lifeTime = 2f;
    protected Animator animator;
    protected Rigidbody2D rigi;
    protected Coroutine rotineAutoDestruct;
    protected Vector3 localScaleX = Vector3.one;
    protected virtual void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }
    public void Init(Vector3 local)
    {
        localScaleX= local;


    }
    protected virtual IEnumerator autoDestruct()
    {
        yield return new WaitForSeconds(_lifeTime);
        this.gameObject.SetActive(false);

    }
    protected virtual void OnDisable()
    {
      
        rigi.velocity = Vector2.zero;
        if (rotineAutoDestruct != null)
            StopCoroutine(rotineAutoDestruct);
        transform.localScale = localScaleX;
    }

    protected virtual void ResetProjectile()
    {
        // Reset any necessary values here
     
        rigi.velocity = new Vector2(moveSpeed.x * localScaleX.x, moveSpeed.y);
        // Reset any other variables as needed
        

    }

    protected virtual void OnEnable()
    {
       
        ResetProjectile();

        rotineAutoDestruct = StartCoroutine(autoDestruct());
        transform.localScale = localScaleX;
    }


    protected virtual void Start()
    {
        rigi.velocity = new Vector2(moveSpeed.x * localScaleX.x, moveSpeed.y);
    }

    protected virtual void HitTarget(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }

        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            Vector2 deliveredKnockBack = transform.localScale.x > 0 ? knockBack : new Vector2(-knockBack.x, knockBack.y);
            //hit the target
            bool gotHit = damageable.Hit(damage, deliveredKnockBack);
            rigi.velocity = Vector2.zero;
            if (gotHit)
            {
               // Debug.Log(collision.name + " hit for " + damage);
            }
            animator.SetTrigger(AnimationStrings.hitTrigger);
        }
    }
}
