using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Build;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string enemyTag = "Enemy";
    private GameObject targetEnemy;
   
    
    void Start()
    {    
        // finds enemy
        targetEnemy = GameObject.FindWithTag(enemyTag);
        Invoke("DeleteSelf", 3f);
    }

    void Update()
    { 

        // deletes bullet if no target, otherwise moves towards target
        if (targetEnemy == null) DeleteSelf();
        else transform.position = Vector3.MoveTowards(transform.position, targetEnemy.transform.position, 5f * Time.deltaTime);
    }

    void DeleteSelf()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == enemyTag)
        { // destroy both enemy and bullet
            GameObject.FindWithTag("Enemy").GetComponent<EnemyFollow>().enemyDamaged(1);
            DeleteSelf();
            
        }
    }
}
