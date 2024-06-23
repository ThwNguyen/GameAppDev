using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : ProjectileBase
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.HitTarget(collision);
        if (collision.CompareTag("Ground")){
            this.transform.gameObject.SetActive(false);
        }
         

    }
}