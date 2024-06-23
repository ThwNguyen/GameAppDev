using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform Target;

    PlayerController player;
    Rigidbody2D playerRG;
    Damageable damageable;

    Vector3 velocity=Vector3.zero;
    [Range(0, 1)]
    public float smoothTime;

    public Vector3 positionOffset;
    public Vector2 xLimit;
    public Vector2 yLimit;
    private void Awake()
    {
        Target = GameManager.Instant.Player.GetComponent<Transform>();
        player = GameManager.Instant.Player;
        playerRG=GameManager.Instant.Player.GetComponent<Rigidbody2D>();
        damageable=GameManager.Instant.Player.GetComponent<Damageable>();
    }
    private void Update()
    {
        if(Target.position.y < yLimit.x-15)
        {  
            damageable.Hit(GameManager.Instant.Player.GetComponent<Damageable>().MaxHealth, Vector2.zero);
            playerRG.velocity = Vector2.zero;
            playerRG.gravityScale = 0;
        }
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 targetPosition=Target.position+positionOffset;
        targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, xLimit.x, xLimit.y), Mathf.Clamp(targetPosition.y, yLimit.x, yLimit.y), - 10);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition,ref velocity,smoothTime);
    }
}
