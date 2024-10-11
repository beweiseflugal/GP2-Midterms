using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform player; 
    [SerializeField] private float spawnInterval = 5f; 
    [SerializeField] private float spawnRadius = 10f; 

    private void Start() {
        GameManager.Instance.OnGameStart += GameManager_OnGameStart;
    }

    private void GameManager_OnGameStart() {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies() {
        while (true) {
                SpawnEnemy();

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy() {
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.y = 0; 
        float distance = spawnRadius + Random.Range(0f, spawnRadius);
        Vector3 spawnPos = player.position + randomDirection.normalized * distance;
        Enemy enemyClone = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
