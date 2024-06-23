using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarE : MonoBehaviour
{
    public Slider hpbarE;
    public Vector3 Offset;


    public void SetHealth(float health, float maxHealth)
    {
        hpbarE.gameObject.SetActive(health < maxHealth);//true
        hpbarE.value = health;
        hpbarE.maxValue = maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        hpbarE.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }
}
