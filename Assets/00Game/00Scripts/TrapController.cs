using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    Damageable damageable;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            damageable = collision.GetComponent<Damageable>();
            damageable.Hit(GameManager.Instant.Player.GetComponent<Damageable>().MaxHealth, Vector2.zero);
            GameManager.Instant.Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GameManager.Instant.Player.GetComponent<Rigidbody2D>().gravityScale = 0;

        }
    }
}
