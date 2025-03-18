using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Build;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string enemyTag = "Enemy";
    private GameObject[] targetEnemies;
    private Transform targetTransform;
    private Vector3 mouseWorldPosition;
    private float smallestDistance = Mathf.Infinity;
    
    void Start()
    {
        // finds enemy
        if (true)
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                mouseWorldPosition = hit.point;
                Debug.Log(hit.point);
            }
        }
        else
        {
            targetEnemies = GameObject.FindGameObjectsWithTag(enemyTag);
            foreach (GameObject enemy in targetEnemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    targetTransform = enemy.transform;
                }
            }
        }

        Invoke("DeleteSelf", 3f);
    }

    void Update()
    {
        if (true) MoveTowardsMouse();
        else MoveTowardsEnemy();

    }

    void MoveTowardsMouse()
    {
        transform.position = Vector3.MoveTowards(transform.position, mouseWorldPosition, 5f * Time.deltaTime);
        if (transform.position == mouseWorldPosition) DeleteSelf();
    }

    void MoveTowardsEnemy()
    {
        if (targetTransform == null) DeleteSelf();
        else transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, 5f * Time.deltaTime);
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
