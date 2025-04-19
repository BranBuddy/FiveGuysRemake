using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Build;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Initialization
    public string enemyTag = "Enemy";
    private GameObject[] targetEnemies;
    private Vector3 targetEnemyPosition;
    private Vector3 mouseWorldPosition;
    private Vector3 movement;
    private float smallestDistance = Mathf.Infinity;
    public bool autoFire;
    public float bulletSpeed = 5f;
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        autoFire = GameObject.FindWithTag("Player").GetComponent<PlayerScript>().autoFire; 
        // finds enemy
        if (autoFire)
        { // set enemies in array if auto firing
            targetEnemies = GameObject.FindGameObjectsWithTag(enemyTag);

            foreach (GameObject enemy in targetEnemies)
            { // get distance to each enemy
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                if (distance < smallestDistance)
                { // change target enemy to closest
                    smallestDistance = distance;
                    targetEnemyPosition = enemy.transform.position;
                }
            }
            
            // get direction
            movement = Vector3.Normalize(targetEnemyPosition - transform.position);
        }
        else
        {
            try
            { // get mouse position using raycast
                Vector3 mouseScreenPosition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                { // get world position
                    mouseWorldPosition = hit.point + Vector3.up;
                }

                // get direction
                movement = Vector3.Normalize(mouseWorldPosition - transform.position);
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }

        // set speed
        movement = movement * Time.deltaTime * bulletSpeed;

        // delete bullet if not hitting
        Invoke("DeleteSelf", 3f);
    }

    void LateUpdate()
    { // move bullet
        rb.MovePosition(transform.position + movement);
    }

    void DeleteSelf()
    { // delete bullet
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == enemyTag)
        { // damage enemy if hitting enemy
            other.GetComponent<EnemyFollow>().enemyDamaged(1);            
        }

        if (other.tag != "Player")
        { // destory self when hitting something that's not player
            Destroy(other.gameObject);
        }
    }
}
