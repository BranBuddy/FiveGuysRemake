using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public string enemyTag = "Enemy";
    private Vector3 targetPosition;
    private Vector3 movementDirection;

    public float bulletSpeed = 1f;
    public bool autoFire;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Get autofire state from player
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            PlayerScript playerScript = player.GetComponent<PlayerScript>();
            if (playerScript != null)
            {
                autoFire = playerScript.autoFire;
            }
        }

        bool hasTarget = false;

        // Determine bullet target
        if (autoFire)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            GameObject closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }

            if (closestEnemy != null)
            {
                targetPosition = closestEnemy.transform.position;
                hasTarget = true;
            }
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                targetPosition = hit.point + Vector3.up; // aim slightly above hit point
                hasTarget = true;
            }
        }

        if (hasTarget)
        {
            // Calculate movement direction
            movementDirection = (targetPosition - transform.position).normalized;
            // Destroy bullet after 3 seconds if it doesn't hit anything
            Invoke(nameof(DeleteSelf), 3f);
        }
        else
        {
            // No target, destroy immediately to prevent rogue bullets
            Debug.Log("No enemy found, bullet destroyed.");
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (movementDirection != Vector3.zero)
        {
            rb.MovePosition(rb.position + movementDirection * bulletSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            // Attempt to damage the enemy
            EnemyFollow enemy = other.GetComponent<EnemyFollow>();
            if (enemy != null)
            {
                enemy.TakeDamage(1); // Use updated method
            }

            DeleteSelf();
        }
    }

    private void DeleteSelf()
    {
        Destroy(gameObject);
    }
}
