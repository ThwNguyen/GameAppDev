using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    [SerializeField] Transform pos;
    [SerializeField] GameObject bossPrefab;

    Transform Player;

    
    public float spawnDistance = 0.5f;

    bool bossSpawned = false;

   
    void Start()
    {
        Player = GameManager.Instant.Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
       
        float distance = Vector3.Distance(Player.position, pos.position);

        if (distance <= spawnDistance && !bossSpawned)
        {
          
            Instantiate(bossPrefab, pos.position, Quaternion.identity);
            bossSpawned = true; 
            pos.gameObject.SetActive(false);
        }
    }
}
