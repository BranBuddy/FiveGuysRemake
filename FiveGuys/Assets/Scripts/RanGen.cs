using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RanGen : MonoBehaviour
{
    public List<GameObject> Spawn = new List<GameObject>();

    void Start()
    {
        // Select a random GameObject from the list
        GameObject GoSpawn = Spawn[Random.Range(0, Spawn.Count)];

        // Generate a random Y-axis rotation (for example, useful for spawning objects with random orientation)
        Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

        // Instantiate the selected GameObject at the current position with the random Y-axis rotation
        GameObject spawnedObject = Instantiate(GoSpawn, transform.position, randomRotation);

        // Ensure rotation is applied (redundant)
        spawnedObject.transform.rotation = randomRotation;

        // Destroy this GameObject after spawning to prevent duplication
        Destroy(gameObject);
    }
}
