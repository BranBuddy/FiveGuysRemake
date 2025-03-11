using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Cinemachine.DocumentationSortingAttribute;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public EnemyHealthBar healthBar;
    public float enemyLives;
    public float maxEnemyLives;
    public int enemyType;
    public float speed;


    private NavMeshAgent enemy;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        enemy = GetComponent<NavMeshAgent>();
        enemy.speed = speed;
        enemyLives = maxEnemyLives;
        healthBar.SetMaxHealth(maxEnemyLives);
        
    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(player.position);
        WhatEnemyTypeAmI();


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && this.gameObject.tag == "Enemy")
        {
            GameObject.Find("Player").GetComponent<Player>().Damage(1);
        }
        else if (other.tag == "Player" && this.gameObject.tag == "Tank")
        {
            GameObject.Find("Player").GetComponent<Player>().Damage(2);
        }
        else if (other.tag == "Player" && this.gameObject.tag == "Rusher")
        {
            GameObject.Find("Player").GetComponent<Player>().Damage(.5f);
        }
    }

    public void WhatEnemyTypeAmI()
    {
        //base enemy
        if(enemyType == 0)
        {
            
        }
        //tank enemy
        else if (enemyType == 1)
        {
            enemy.speed = 3;
            maxEnemyLives = 5;
        }
        //rushdown enemy
        else if (enemyType == 2)
        {
            maxEnemyLives = 2;
            enemy.speed = 10 - (enemyLives * 2);
            
        }
    }

    public void enemyDamaged(int howMuch)
    {
        enemyLives -= howMuch;
        healthBar.SetHealth(enemyLives);

        if(enemyLives <= 0)
        {
            Destroy(this.gameObject);
            GameObject.Find("Player").GetComponent<Player>().EarnXP(1);
        }
    }
}