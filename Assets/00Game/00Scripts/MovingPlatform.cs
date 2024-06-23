using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool onGround = false;
    public float speed;
    Vector3 targetPos;

    PlayerController playerController;
    Rigidbody2D rigi;
    Vector3 moveDirection;
    Rigidbody2D playerRB;

    public GameObject waypointContainer;
    private Transform[] waypoints;
    private int currentWaypointIndex;
    private int waypointCount;
    private int direction = 1;

    public float waitDuration;

    private Transform newParent;
    private Transform oldParent;

    private void Awake()
    {
        playerController = GameManager.Instant.Player.GetComponent<PlayerController>();
        rigi = GetComponent<Rigidbody2D>();
        playerRB = GameManager.Instant.Player.GetComponent<Rigidbody2D>();
        waypoints = new Transform[waypointContainer.transform.childCount];
        for (int i = 0; i < waypointContainer.transform.childCount; i++)
        {
            waypoints[i] = waypointContainer.transform.GetChild(i);
        }
    }

    private void Start()
    {
        currentWaypointIndex = 1;
        waypointCount = waypoints.Length;
        targetPos = waypoints[1].transform.position;

        DirectionCalculate();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
        {
            NextWaypoint();
        }
        if (newParent != oldParent)
        {
            playerController.transform.SetParent(newParent);
            oldParent = newParent;
        }
    }

    private void FixedUpdate()
    {
        rigi.velocity = moveDirection * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController.isOnPlatform = true;
            playerController.platformrg = rigi;
            newParent = transform;
            if (playerController.isOnPlatform)
                playerRB.gravityScale = 20f;
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            newParent = null;
            playerController.isOnPlatform = false;
            playerRB.gravityScale = 1f;
        }
    }

    private void NextWaypoint()
    {
        transform.position = targetPos;
        moveDirection = Vector3.zero;
        if (currentWaypointIndex == waypointCount - 1)
        {
            direction = -1;
        }
        if (currentWaypointIndex == 0)
        {
            direction = 1;
        }
        currentWaypointIndex += direction;
        targetPos = waypoints[currentWaypointIndex].transform.position;
        StartCoroutine(WaitForNextWaypoint());
    }

    private IEnumerator WaitForNextWaypoint()
    {
        yield return new WaitForSeconds(waitDuration);
        DirectionCalculate();
    }

    private void DirectionCalculate()
    {
        moveDirection = (targetPos - transform.position).normalized;

    }
}
