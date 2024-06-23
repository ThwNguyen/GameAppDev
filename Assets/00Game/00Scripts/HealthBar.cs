using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public TMP_Text healthBarText;
    public Slider healthBarSlider;
    Damageable playerDamageable;


    private void Awake()
    {

        playerDamageable = GameManager.Instant.Player.GetComponent<Damageable>();
    }
    // Start is called before the first frame update
    void Start()
    {
        healthBarSlider.value = CalculateSliderPercenttage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthBarText.text = "HP : " + playerDamageable.Health + "/" + playerDamageable.MaxHealth;

    }

    private float CalculateSliderPercenttage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }
    private void OnEnable()
    {
        playerDamageable.changeHealth.AddListener(OnPlayerHealthChange);
    }
    private void OnDisable()
    {
        playerDamageable.changeHealth.RemoveListener(OnPlayerHealthChange);
    }

    public void OnPlayerHealthChange(float newHealth, float maxHealth)
    {
        healthBarSlider.value = CalculateSliderPercenttage(newHealth, maxHealth);
        healthBarText.text = "HP : " + newHealth + "/" + maxHealth;
    }


}
