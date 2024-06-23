using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]

    public class MushroomController : EnemyBase
    {


        protected override void Awake()
        {

            base.Awake();


        }

        protected override void Start()
        {

            base.Start();
        HPBar.SetHealth(damageable.Health, damageable.MaxHealth);

    }

        protected override void FixedUpdate()
        {

            base.FixedUpdate();


        }

        protected override void Update()
        {

            base.Update();
        HPBar.SetHealth(damageable.Health, damageable.MaxHealth);

    }
        public override void OnHit(float dmg, Vector2 knockBack)
        {

            rigi.velocity = new Vector2(knockBack.x, rigi.velocity.y + knockBack.y);
        }
        public override void OnCliffDetection()
        {
            if (touchingDirection.IsGrounded)
            {
                FlipDirection();
            }
        }

    }

