using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
     private float horizontalInput;
     private float verticalInput;
    public float speed = 5f;
    public float lives = 3f;
    public float maxLives = 3f;
    public float minXP = 0f;
    public float xp;
    public GameObject enemy;
    public GameObject bullet;

    public HealthBar healthBar;
    public XPBar xpBar;



    void Start()
    {
        xp = minXP;
        lives = maxLives;
        healthBar.SetMaxHealth(maxLives);
        xpBar.SetMinXP(minXP);

    }
   
    // Update is called once per frame
    void Update()
    {
        Movement();
        HurtSelf();
        ShootBullet();
    }
    
    //moves player
    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * Time.deltaTime * speed);
    }

    //does damage to player in 1/3 increments
    void Damage()
    {
        lives--;

        healthBar.SetHealth(lives);

        if (lives == 0)
        {
            Destroy(this.gameObject);
        }
    }

    //Temp function to showcase xp bar
    void KillEnemy()
    {
        Destroy(enemy.gameObject);
        xp += .5f;

        xpBar.SetXP(xp);
    }

    //Temp function to show health bar
    void HurtSelf()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damage();
        }
    }

    //for now having the enemy die when collided to show off xp bar
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            KillEnemy();
        }
    }

    void ShootBullet()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }
}
