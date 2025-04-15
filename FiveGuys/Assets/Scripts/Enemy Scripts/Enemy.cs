using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;

    private float countdown = 5f;

    private Spawner waveSpawner;

    private void Start()
    {
        waveSpawner = GetComponentInParent<Spawner>();
    }
    private void Update()
    {

        countdown -= Time.deltaTime;

        if (countdown <= 0)
        {
            Destroy(gameObject);

            waveSpawner.waves[waveSpawner.currentWaveIndex].enemiesLeft--;
        }
    }

   
}