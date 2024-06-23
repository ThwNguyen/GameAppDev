using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRange : MonoBehaviour
{
    [SerializeField]
    private bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        set { _hasTarget = value; }
    }

    private Transform playerTransform;

    public Vector3 playerPosition;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            HasTarget = true;

            playerTransform = other.transform;

           
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            HasTarget = false;

         
            playerTransform = null;
        }
    }

    private void Update()
    {
       
        if (_hasTarget && playerTransform != null)
        {
           
             playerPosition = playerTransform.position;
           
        }
    }
}
