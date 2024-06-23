 using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Bomb : ProjectileBase
{
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
            animator.SetTrigger(AnimationStrings.hitTrigger);

        
    }

}
