using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent noCollidersRemain;

    public bool canFlip=false;

    public List<Collider2D> detectionColliders;
    Collider2D collider;
    private void Awake()
    {
        detectionColliders = new List<Collider2D>();
        collider = this.GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectionColliders.Add(collision);
        canFlip = false;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        detectionColliders.Remove(collision);
        canFlip = true;
        noCollidersRemain.Invoke();
    }
}
