using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    CountDownTime countDownTime;
    public Image skill1;
    public Image skill2;
    public Image skill3;
    public Image skill4;
    // Start is called before the first frame update
    void Start()
    {
        countDownTime=GameManager.Instant.Player.GetComponent<CountDownTime>();
       
    }

    // Update is called once per frame
    void Update()
    {//1
        if (countDownTime.CanAttack)
        {
            skill1.fillAmount = 1;
        }
        else
        {
            skill1.fillAmount = countDownTime.attackTimer / countDownTime.attackCooldown;
        }
        //2
        if (countDownTime.CanFireBow) {
            skill2.fillAmount = 1;
        }
        else
        {
            skill2.fillAmount = countDownTime.fireBowTimer / countDownTime.fireBowCooldown;
        }
        //3
        if (countDownTime.CanRainOfArrows)
        {
            skill3.fillAmount = 1;
        }
        else
        {
            skill3.fillAmount = countDownTime.rainOfArrowsTimer / countDownTime.rainOfArrowsCooldown;
        }
        //4
        if (countDownTime.CanSPLaze)
        {
            skill4.fillAmount = 1;
        }
        else
        {
            skill4.fillAmount = countDownTime.splazeTimer / countDownTime.splazeCooldown;
        }

    }
}
