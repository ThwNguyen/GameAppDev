using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour,IGetHit
{
    public UnityEvent<float, Vector2> damageableHit;
    public UnityEvent damageableDeath;
    public UnityEvent<float, float> changeHealth;
    Animator animator;
    private GameManager gameManager;

    [SerializeField]
    private float _armor = 0;
    public float Armor
    {
        get
        {
            return _armor;
        }
        set
        {
            _armor = value;
        }
    }
    //mau toi da
    [SerializeField]
    private float _maxHealth = 100;
    public float MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    //mau thuc the
    [SerializeField]
    private float _health = 100;
    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            //hpbar
            changeHealth?.Invoke(_health, MaxHealth);
            if (_health >= MaxHealth)
            {
                _health = MaxHealth;
            }
            if (_health <= 0)
            {
              
                IsAlive = false;
                if (this.transform.CompareTag("Enemy")||this.transform.CompareTag("Slime")||this.transform.CompareTag("Slime"))
                {
                    gameManager.killed++; 
                    gameManager.onEndGame.Invoke(); 
                }
                if (this.transform.CompareTag("Boss"))
                {
                 
                    gameManager.boss_killed++; 
                    gameManager.onEndGame.Invoke();
                }
            }
        }
    }


    private bool _isAlive = true;
    private bool isInvincible = false;//bat kha chien bai

    public float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;
    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);

            if (value == false)
            {
                damageableDeath.Invoke();
            }

        }
    }

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        gameManager = GameManager.Instant;
    }
    void Update()
    {

        if (isInvincible)
        {//neu dang vo dich
            if (timeSinceHit > invincibilityTime)
            {
                //remove invincibility;
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;

        }

    }

    public bool Hit(float damage, Vector2 knockBack)
    {

        if (IsAlive && !isInvincible)
        {
            isInvincible = true;
             
           
             if((damage - Armor) > 0)
            {
                CharacterEvents.characterDamaged.Invoke(gameObject, damage - Armor);
                Health -= damage - Armor;
                LockVelocity = true;

                animator.SetTrigger(AnimationStrings.hitTrigger);
               
                damageableHit?.Invoke(damage, knockBack);
                return true;
            }
            
        }
      
        return false;
    }

    
    public bool Heal(float healthRestore)
    {//hoi mau
        if (IsAlive && Health < MaxHealth)
        {  

            //kiem tra luong mau da mat
            float maxHeal = Mathf.Max(MaxHealth - Health, 0);
            //luong mau co the hoi
            float actualHeal = Mathf.Min(maxHeal, healthRestore);

            Health += actualHeal;
                CharacterEvents.characterHealed.Invoke(this.gameObject, actualHeal);
            return true;
        }

        return false;
    }
}