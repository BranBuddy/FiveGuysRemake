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
    public float maxEnemyLives = 3f;

    private NavMeshAgent enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        enemyLives = maxEnemyLives;
        healthBar.SetMaxHealth(maxEnemyLives);
    }

    // Update is called once per frame
    void Update()
    {
        enemy.destination = player.position;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<Player>().Damage(1);
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