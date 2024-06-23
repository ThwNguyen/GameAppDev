using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem movementParticle;

    [SerializeField] ParticleSystem fallParticle;


    [Range(0, 10)]
    [SerializeField] int occurAfterVelocity;


    [Range(0, 0.2f)]
    [SerializeField] float dustFormationPeriod;

    [SerializeField] Rigidbody2D playerRigi;

    float counter;

    // Update is called once per frame
    void Update()
    {  
        counter += Time.deltaTime;

        if(Mathf.Abs(playerRigi.velocity.x) > occurAfterVelocity &&GameManager.Instant.Player.touchingDirections.IsGrounded )
        {
            counter = 0;
            movementParticle.Play();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.CompareTag("Ground") ){ 
        
            fallParticle.Play();
        }
    }
    
}
