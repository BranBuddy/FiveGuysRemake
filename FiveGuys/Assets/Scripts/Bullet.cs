using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Build;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string enemyTag = "Enemy";
    public float bulletSpeed = 10f;
    private float minDistance = Mathf.Infinity;
    private Transform targetEnemy = null;
    private Vector3 mouseTarget;
    public Player player;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        if (player.autoFire == true)
        { // creates list of enemies
            GameObject[] targetEnemies = GameObject.FindGameObjectsWithTag(enemyTag);
            if (targetEnemies.Length > 0)
            {
                foreach (GameObject enemy in targetEnemies)
                { // gets distance to enemies
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);

                    if (distance < minDistance)
                    { // changes target based on minimum distance found
                        targetEnemy = enemy.transform;
                        minDistance = distance;
                    }
                }
            }
        }
        else
        { // gets mouse position in screen as
            Vector2 mousePos = new Vector2();
            Event currentEvent = Event.current;

            mousePos.x = currentEvent.mousePosition.x;
            mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

            mouseTarget = cam.ScreenToWorldPoint(new Vector3(mousePos.x, 1, mousePos.y));
        }
        Invoke("DeleteSelf", 5f);
    }

    void Update()
    {
        // deletes bullet if no target
        if (targetEnemy == null) DeleteSelf();
        
        if (player.autoFire == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetEnemy.position, bulletSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, mouseTarget, bulletSpeed * Time.deltaTime);
        }

    }

    void DeleteSelf()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == enemyTag)
        { // damage enemy and destroy bullet
            GameObject.FindWithTag("Enemy").GetComponent<EnemyFollow>().enemyDamaged(1);
            DeleteSelf();
            
        }
    }
}
