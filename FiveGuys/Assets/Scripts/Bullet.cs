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
    public Player player;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        // finds enemy position
        if (player.autoFire == true)
        {
            GameObject[] targetEnemies = GameObject.FindGameObjectsWithTag(enemyTag);
            if (targetEnemies.Length > 0)
            {
                foreach (GameObject enemy in targetEnemies)
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);

                    if (distance < minDistance)
                    {
                        targetEnemy = enemy.transform;
                        minDistance = distance;
                    }
                }
            }
        }
        Invoke("DeleteSelf", 5f);
    }

    void Update()
    {

        // deletes bullet if no target, otherwise moves towards target
        if (player.autoFire == true)
        {
            if (targetEnemy == null) DeleteSelf();
            else transform.position = Vector3.MoveTowards(transform.position, targetEnemy.transform.position, bulletSpeed * Time.deltaTime);
        }

        Vector2 mousePos = new Vector2();
        Event currentEvent = Event.current;

        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        Vector3 target

    }

    void DeleteSelf()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == enemyTag)
        { // destroy both enemy and bullet
            //Destroy(other.gameObject);
            DeleteSelf();
        }
    }
}
