using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public GameObject ends;
    [SerializeField] PlayerController _player;
    public PlayerController Player => _player;

    public int monster_limit = 0;
    public int killed = 0;
    public int boss_limit = 1;
    public int boss_killed = 0;

    public UnityEvent onEndGame; 

    private bool hasGameEnded = false; 

  
    void Start()
    {
      
        onEndGame.AddListener(CheckEndGame);
    }

  


    void CheckEndGame()
    {
        if (!hasGameEnded && killed >= monster_limit && boss_killed >= boss_limit)
        {
            
            StartCoroutine(EndGameAfterDelay(1.0f)); 
            hasGameEnded = true;
        }
    }

    IEnumerator EndGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        ends.SetActive(true);
    }
}