using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
   
    public int attackDamage = 10;
    Damageable damageable;
    public Vector2 knockBack = Vector2.zero;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        damageable = collision.GetComponent<Damageable>();
        //See if it can be hit
        if (damageable != null)
        {
            Vector2 deliveredKnockBack = transform.parent.localScale.x > 0 ? knockBack : new Vector2(-knockBack.x, knockBack.y);
            //hit the target
            bool gotHit = damageable.Hit(attackDamage, knockBack);
           
        }
    }
}