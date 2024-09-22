using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;     
    public Transform player;               
    public float minSpawnDistance = 10f;   
    public float maxSpawnDistance = 30f;   
    public int maxAttempts = 10;          

    private List<GameObject> _activeEnemies = new List<GameObject>();
    private int _minEnemyCount = 5;

    private void Update()
    {
        _activeEnemies.RemoveAll(enemy => enemy == null);

        while (_activeEnemies.Count < _minEnemyCount)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = GetValidSpawnPosition();

        if (spawnPosition != Vector3.zero)
        {
            GameObject selectedEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            GameObject newEnemy = Instantiate(selectedEnemyPrefab, spawnPosition, Quaternion.identity);
           // selectedEnemyPrefab.transform.position = new Vector3(selectedEnemyPrefab.transform.position.x, 0, selectedEnemyPrefab.transform.position.z);
            _activeEnemies.Add(newEnemy);
        }
    }

    private Vector3 GetValidSpawnPosition()
    {
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Vector3 randomDirection = Random.insideUnitSphere.normalized;

            float spawnDistance = Random.Range(minSpawnDistance, maxSpawnDistance);

            Vector3 spawnPosition = player.position + randomDirection * spawnDistance;

            spawnPosition.y = 0;

            return spawnPosition;
        }

        return Vector3.zero;
    }
}
