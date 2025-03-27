using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Build;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
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
        {
            targetEnemies = GameObject.FindGameObjectsWithTag(enemyTag);

            if (targetEnemies == null) DeleteSelf();

            foreach (GameObject enemy in targetEnemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    targetEnemyPosition = enemy.transform.position;
                }
            }

            movement = Vector3.Normalize(targetEnemyPosition - transform.position);
        }
        else
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                mouseWorldPosition = hit.point;
            }

            movement = Vector3.Normalize(mouseWorldPosition - transform.position);
        }

        movement = movement * Time.deltaTime * bulletSpeed;

        Invoke("DeleteSelf", 3f);
    }

    void LateUpdate()
    {
        rb.MovePosition(transform.position + movement);
    }

    void DeleteSelf()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == enemyTag)
        { // destroy both enemy and bullet
            other.GetComponent<EnemyFollow>().enemyDamaged(1);

            DeleteSelf();
            
        }
    }
}
