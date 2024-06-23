using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialTrapController : MonoBehaviour
{
    Damageable damageable;
    public float dmg=5;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            damageable = collision.GetComponent<Damageable>();
            damageable.Hit(dmg, Vector2.zero);


        }
    }
}
