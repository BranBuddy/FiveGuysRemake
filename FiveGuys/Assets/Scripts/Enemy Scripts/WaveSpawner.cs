using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<GameObject> enemiesToSpawn; // List of enemies to spawn
    public string spawnPointTag = "SpawnPoint"; // Tag to identify spawn points
    public int initialSpawnCount = 3; // Number of objects in the first wave
    public float cooldownTime = 5f; // Time between waves
    public float spawnDelay = 0.2f; // Delay between each object spawn
    public float growthFactor = 2f; // The exponential growth factor for objects per wave

    private int currentWave = 0;
    private int objectsPerWave;
    private List<GameObject> activeObjects = new List<GameObject>();
    private Transform[] spawnPoints; // Array of spawn points

    // Variable to store the enemy type for the first wave
    private GameObject firstWaveEnemy;

    void Start()
    {
        // Automatically find all spawn points in the scene by tag
        GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag(spawnPointTag);
        spawnPoints = new Transform[spawnPointObjects.Length];

        // Add each spawn point to the spawnPoints array
        for (int i = 0; i < spawnPointObjects.Length; i++)
        {
            spawnPoints[i] = spawnPointObjects[i].transform;
        }

        // Debug: Log the number of spawn points found
        Debug.Log($"Found {spawnPoints.Length} spawn points.");

        objectsPerWave = initialSpawnCount;
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (true)
        {
            // Wait until all objects are destroyed before proceeding to the next wave
            yield return new WaitUntil(() => activeObjects.Count == 0);
            Debug.Log($"All objects destroyed. Proceeding to wave {currentWave + 1}.");

            // Wait for the cooldown time before starting the next wave
            yield return new WaitForSeconds(cooldownTime);

            currentWave++;

            // For the first wave, always spawn the first enemy in the list
            if (currentWave == 1)
            {
                // Set the first wave enemy to the first enemy in the list
                firstWaveEnemy = enemiesToSpawn[0];
                objectsPerWave = initialSpawnCount;
            }
            else
            {
                // Exponential increase in the number of objects per wave (after the first wave)
                objectsPerWave = Mathf.FloorToInt(initialSpawnCount * Mathf.Pow(growthFactor, currentWave - 1));
            }

            Debug.Log($"Wave {currentWave} will spawn {objectsPerWave} objects (exponential growth for waves after the first).");

            for (int i = 0; i < objectsPerWave; i++)
            {
                // Select a random spawn point from the array
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Debug.Log($"Spawning object {i + 1}/{objectsPerWave} at spawn point {spawnPoint.name}.");

                // For wave 1, always spawn the first enemy in the list
                GameObject enemyToSpawn = (currentWave == 1) ? firstWaveEnemy : enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)];

                // Instantiate the selected enemy at the spawn point
                GameObject obj = Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);
                activeObjects.Add(obj);

                // Track destruction of objects
                obj.AddComponent<ObjectTracker>().SetSpawner(this);

                // Wait for a short time before spawning the next object
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }

    public void ObjectDestroyed(GameObject obj)
    {
        activeObjects.Remove(obj);

        // Debug: Log when an object is destroyed and removed from the active list
        Debug.Log($"Object {obj.name} destroyed and removed from active objects. Active count: {activeObjects.Count}.");
    }
}

// Helper class to notify the spawner when an object is destroyed
public class ObjectTracker : MonoBehaviour
{
    private WaveSpawner spawner;

    public void SetSpawner(WaveSpawner spawner)
    {
        this.spawner = spawner;
    }

    private void OnDestroy()
    {
        if (spawner != null)
        {
            // Debug: Log when an object is destroyed in the ObjectTracker
            Debug.Log($"Object {gameObject.name} is being destroyed and notifying the spawner.");
            spawner.ObjectDestroyed(gameObject);
        }
    }
}
