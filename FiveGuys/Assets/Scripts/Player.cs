using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
     private float horizontalInput;
     private float verticalInput;
    public float speed = 5f;
    public float lives = 3f;
    public float maxLives = 3f;
    public float minXP = 0f;
    public float xp;
    public bool autoFire = false;
    public GameObject enemy;
    public GameObject bullet;
    private int charLevel;

<<<<<<< HEAD
    //public HealthBar healthBar;
    //public XPBar xpBar;
=======
    public Healthbar healthBar;
    public XPBar xpBar;
>>>>>>> main

    public TextMeshProUGUI levelUpText;

    void Start()
    {
        charLevel = 1;
        levelUpText.text = "Level: " + charLevel;
        xp = minXP;
        lives = maxLives;
<<<<<<< HEAD
        //healthBar.SetMaxHealth(maxLives);
        //xpBar.SetMinXP(minXP);

=======
        healthBar.SetMaxHealth(maxLives);
        xpBar.SetMinXP(minXP);
        
>>>>>>> main
    }
   
    // Update is called once per frame
    void Update()
    {
        Movement();
<<<<<<< HEAD
        HurtSelf();
        if (Input.GetKeyDown(KeyCode.Mouse0) && autoFire == false)
        {
            ShootBullet();
        }
        if (Input.GetKeyDown(KeyCode.E) && autoFire == false)
        {
            StartCoroutine("Autofire");
            autoFire = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && autoFire == true)
        {
            StopCoroutine("Autofire");
            autoFire = false;
        }
=======
        ShootBullet();
>>>>>>> main
    }
    
    //moves player
    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * Time.deltaTime * speed);
    }

    //does damage to player in 1/3 increments
     public void Damage(float damageAmount)
    {
<<<<<<< HEAD
        lives--;

       // healthBar.SetHealth(lives);
=======
        lives -= damageAmount;
        healthBar.SetHealth(lives);
>>>>>>> main

        if (lives <= 0)
        {
            
            Destroy(this.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }

    public void EarnXP(float xpAmount)
    {
<<<<<<< HEAD
        Destroy(enemy.gameObject);
        xp += .5f;

        //xpBar.SetXP(xp);
    }
=======
        xp = xp + ((xpAmount / charLevel) * .5f);
        xpBar.SetXP(xp);
>>>>>>> main

        if (xp >= 1)
        {
            xp = minXP;
            xpBar.SetXP(xp);
            Debug.Log("lEVEL UP");
            LevelUp();

        }
    }

    public void LevelUp()
    {

        if (charLevel > 1)
        {
            charLevel++;
            levelUpText.text = "Level: " + charLevel;

        } else
        {
            charLevel = 2;
            levelUpText.text = "Level: " + charLevel;
        }
    }

    void ShootBullet()
    {
        Instantiate(bullet, transform.position, transform.rotation);        
    }

    IEnumerator Autofire()
    {
        while (true)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            Debug.Log("Fired");
            yield return new WaitForSeconds(0.5f);
        }
    }
}
