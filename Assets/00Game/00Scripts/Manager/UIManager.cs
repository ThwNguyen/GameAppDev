using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public Button pauseBtn;
    public GameObject loseMenu;
    public GameObject winMenu;

    public Canvas gameCanvas;

    private void Awake()
    {
        gameCanvas = FindAnyObjectByType<Canvas>();

    }
    private void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;
        CharacterEvents.lost += HandleLoseMenu;
        CharacterEvents.won += HandleWinMenu;
    }
    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed -= CharacterHealed;
        CharacterEvents.lost -= HandleLoseMenu;
        CharacterEvents.won -= HandleWinMenu;
    }
    public void CharacterTookDamage(GameObject character, float damageReceived)
    {//mat mau
       
        Vector3 spawnPos = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPos, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        tmpText.text = damageReceived.ToString();
    }
    public void CharacterHealed(GameObject character, float healthRestored)
    {//hoi mau
       
        Vector3 spawnPos = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPos, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        tmpText.text = healthRestored.ToString();
    }
    public void HandleLoseMenu()
    {
        StartCoroutine(ShowLoseMenuDelayed(loseMenu));
    }
    public void HandleWinMenu()
    {
        StartCoroutine(ShowLoseMenuDelayed(winMenu));
    }

    private IEnumerator ShowLoseMenuDelayed(GameObject menu)
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1f);

        // Activate the loseMenu
        menu.SetActive(true);

        // Deactivate the pauseBtn
        pauseBtn.gameObject.SetActive(false);
    }
}




