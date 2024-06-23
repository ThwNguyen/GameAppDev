using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTime : MonoBehaviour
{
    public float attackTimer = 0f;
    public float airAttackTimer = 0f;
    public float fireBowTimer = 0f;
    public float rainOfArrowsTimer = 0f;
    public float splazeTimer = 0f;

    // Target cooldown times for each skill
    [SerializeField]
   public float attackCooldown = 1f;
    [SerializeField]
    public float airAttackCooldown = 1f;
    [SerializeField]
    public float fireBowCooldown = 2f;
    [SerializeField]
    public  float rainOfArrowsCooldown = 5f;
    [SerializeField]
    public  float splazeCooldown = 15f;

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

    [SerializeField]
    private bool _canAttack = true;
    public bool CanAttack
        {
            get
            {
                return _canAttack;
            }
            set
            {
                _canAttack = value;
            }
        }

    private bool _canFireBow = true;
    public bool CanFireBow
        {
            get
            {
                return _canFireBow;
            }
            set
            {
                _canFireBow = value;
            }
        }

    private bool _canRainOfArrows = true;
    public bool CanRainOfArrows
        {
            get
            {
                return _canRainOfArrows;
            }
            set
            {
                _canRainOfArrows = value;
            }
        }

    private bool _canSPLaze = true;
    public bool CanSPLaze
        {
            get
            {
                return _canSPLaze;
            }
            set
            {
                _canSPLaze = value;
            }
        }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            // Countdown timer for Attack skill
            if (!_canAttack)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackCooldown)
                {
                    _canAttack = true;
                    attackTimer = 0f;
                }
            }
        // Countdown timer for AirAttack skill
        if (!_canAirAttack)
        {
            airAttackTimer += Time.deltaTime;
            if (airAttackTimer >= airAttackCooldown)
            {
                _canAirAttack = true;
                airAttackTimer = 0f;
            }
        }

        // Countdown timer for FireBow skill
        if (!_canFireBow)
            {
                fireBowTimer += Time.deltaTime;
                if (fireBowTimer >= fireBowCooldown)
                {
                    _canFireBow = true;
                    fireBowTimer = 0f;
                }
            }

            // Countdown timer for RainOfArrows skill
            if (!_canRainOfArrows)
            {
                rainOfArrowsTimer += Time.deltaTime;
                if (rainOfArrowsTimer >= rainOfArrowsCooldown)
                {
                    _canRainOfArrows = true;
                    rainOfArrowsTimer = 0f;
                }
            }

            // Countdown timer for SPLaze skill
            if (!_canSPLaze)
            {
                splazeTimer += Time.deltaTime;
                if (splazeTimer >= splazeCooldown)
                {
                    _canSPLaze = true;
                    splazeTimer = 0f;
                }
            }
        }
}
