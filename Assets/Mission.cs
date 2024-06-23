using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textE, textB;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instant;
    }

    void Update()
    {
        // Display the values of killed and monster_limit
        textE.text = "Killed: " + gameManager.killed.ToString()+"/" + gameManager.monster_limit.ToString();
        textB.text = "Boss: " + gameManager.boss_killed.ToString()+"/"+gameManager.boss_limit.ToString();
    }
}
