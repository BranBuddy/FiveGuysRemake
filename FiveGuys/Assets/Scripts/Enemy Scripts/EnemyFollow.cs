using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public GameObject player;
    public EnemyHealthBar healthBar;
    public float enemyLives;
    public float maxEnemyLives;
    public EnemyType enemyType;
    public float speed = 5f;
    public AudioClip deathClip;
    public float deathVolume = 0.7f;

    private NavMeshAgent enemy;
    private PlayerScript playerScript;

    public enum EnemyType
    {
        Base,
        Tank,
        Rushdown
    }

    void Start()
    {
        // Initialize components
        enemy = GetComponent<NavMeshAgent>();

        // Find the player object by tag
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("Player found");

        if (player != null)
        {
            playerScript = player.GetComponent<PlayerScript>();
        }

        // Initialize stats based on enemy type
        InitializeEnemyStats();
        enemyLives = maxEnemyLives;
        healthBar.SetMaxHealth(maxEnemyLives);
    }

    void Update()
    {
        // Move enemy towards player if player is set
        if (player != null)
        {
            enemy.SetDestination(player.transform.position);
        }

        HandleEnemyBehavior();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Handle interactions with the player
        if (other.CompareTag("Player") && playerScript != null)
        {
            switch (enemyType)
            {
                case EnemyType.Base:
                    playerScript.Damage(1);
                    break;
                case EnemyType.Tank:
                    playerScript.Damage(2);
                    break;
                case EnemyType.Rushdown:
                    playerScript.Damage(0.5f);
                    break;
            }
        }
    }

    private void InitializeEnemyStats()
    {
        // Set enemy stats based on enemy type
        switch (enemyType)
        {
            case EnemyType.Base:
                maxEnemyLives = 3;
                speed = 5f;
                break;
            case EnemyType.Tank:
                maxEnemyLives = 5;
                speed = 3f;
                break;
            case EnemyType.Rushdown:
                maxEnemyLives = 2;
                break;
        }

        if (enemy != null)
        {
            enemy.speed = speed;
        }
    }

    private void HandleEnemyBehavior()
    {
        // Handle enemy behavior when health reaches 0
        if (enemyLives <= 0)
        {
            Die();
        }

        // For Rushdown enemy type, adjust speed based on health
        if (enemyType == EnemyType.Rushdown)
        {
            enemy.speed = Mathf.Max(3f, 10f - (enemyLives * 2));
        }
    }

    private void Die()
    {
        // Play death sound and give XP to player
        AudioSource.PlayClipAtPoint(deathClip, transform.position, deathVolume);
        if (playerScript != null)
        {
            switch (enemyType)
            {
                case EnemyType.Base:
                    playerScript.EarnXP(0.5f);
                    break;
                case EnemyType.Tank:
                    playerScript.EarnXP(1f);
                    break;
                case EnemyType.Rushdown:
                    playerScript.EarnXP(1.5f);
                    break;
            }
        }

        Destroy(gameObject);
    }

    // Method to handle enemy damage
    public void TakeDamage(int amount)
    {
        enemyLives -= amount;
        healthBar.SetHealth(enemyLives);
    }
}
