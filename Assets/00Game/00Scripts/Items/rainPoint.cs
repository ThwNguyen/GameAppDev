using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainPoint : Singleton<rainPoint>
{
    public List<Transform> enemiesInRange = new List<Transform>();

    private bool _hasTarget = false;
    public float posy = 3f;
    public bool HasTarget
    {
        get { return _hasTarget; }
        set { _hasTarget = value; }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy") || other.CompareTag("Boss")||other.CompareTag("Slime"))
        {

            enemiesInRange.Add(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss") || other.CompareTag("Slime"))
        {
            enemiesInRange.Remove(other.transform);
        }
    }

    private void Update()
    {
        if (enemiesInRange.Count > 0)
        {
            HasTarget = true;
        }
        else
        {
            HasTarget = false;
        }

    }


    public Vector3 GetClosestEnemyPosition()
    {
        if (enemiesInRange.Count == 0)
        {
            return Vector3.zero;
        }

        Vector3 closestEnemyPosition = Vector3.zero;
        float closestDistance = float.MaxValue;

        foreach (Transform enemy in enemiesInRange)
        {
            float distance = Vector2.Distance(transform.position, enemy.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemyPosition = enemy.position;

                // Kiểm tra tag của mục tiêu và cộng hoặc trừ giá trị theo điều kiện
                if (enemy.CompareTag("Enemy"))
                {
                    closestEnemyPosition.y += 2.2f;
                }
                else if (enemy.CompareTag("Boss"))
                {
                    closestEnemyPosition.y -= posy;
                }
                else if (enemy.CompareTag("Slime"))
                {
                    closestEnemyPosition.y += 3.2f;
                }
            }
        }

        return closestEnemyPosition;
    }
}