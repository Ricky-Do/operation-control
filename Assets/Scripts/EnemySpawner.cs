using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;     // Reference to the enemy prefab
    private float spawnInterval = 3f;   // Time interval between spawns
    [SerializeField] Transform[] spawnPoints;    // Array of spawn points

    void Start()
    {
        // Start spawning enemies
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Randomly select a spawn point
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instantiate an enemy at the selected spawn point
        Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
    }
}
