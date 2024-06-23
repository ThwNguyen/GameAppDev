using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnSlime : MonoBehaviour
{
    [SerializeField] GameObject pos;
    [SerializeField] GameObject bossPrefab;

    Transform Player;

    public float posY = 3f;
    public float delayBeforeSpawn = 1f;
    bool bossSpawned = false;

    void Start()
    {
        Player = GameManager.Instant.Player.transform;
    }

    void Update()
    {
        if (!pos.gameObject.activeSelf && !bossSpawned)
        {
            StartCoroutine(SpawnBossWithDelay());
            bossSpawned = true;
        }
    }
        IEnumerator SpawnBossWithDelay()
        {
            yield return new WaitForSeconds(delayBeforeSpawn);

            Vector3 spawnPosition = pos.transform.position;
            spawnPosition.y += posY;
            Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
          

        }
    
}