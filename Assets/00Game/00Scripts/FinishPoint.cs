using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UnlockNewLevel();
        }
    }
    void UnlockNewLevel()
    {
        
         
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt(AnimationStrings.ReachedIndex))
        {
          
            PlayerPrefs.SetInt(AnimationStrings.ReachedIndex, SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetInt(AnimationStrings.UnlockedLevel, SceneManager.GetActiveScene().buildIndex+1);
            PlayerPrefs.Save();
          
        }
        CharacterEvents.won.Invoke();
    }
}
