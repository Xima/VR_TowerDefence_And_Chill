using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject[] enemy;
    public float minSpawnTime = 2f, maxSpawnTime = 5f;
    public Transform[] spawnPoints;

    private static List<EnemyHealth> enemies;
    private bool isSpawning = false;

    void Start()
    {
        enemies = new List<EnemyHealth>();
    }

    public void startSpawn()
    {
        isSpawning = true;
        Invoke("Spawn", minSpawnTime);
    }

    public void stopSpawn()
    {
        isSpawning = false;
    }

    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);
        int enemyIndex = Random.Range(0, enemy.Length);

        GameObject go = Instantiate(enemy[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            
        enemies.Add(go.GetComponent<EnemyHealth>());

        float randTime = Random.Range(minSpawnTime, maxSpawnTime);
        if (isSpawning)
            Invoke("Spawn", randTime);
    }

    public static List<EnemyHealth> getEnemies()
    {
        return enemies;
    }
}
