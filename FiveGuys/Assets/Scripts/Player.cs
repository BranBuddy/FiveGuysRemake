using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private int charLevel;

    public Healthbar healthBar;
    public XPBar xpBar;

    public TextMeshProUGUI levelUpText;



    void Start()
    {
        charLevel = 1;
        levelUpText.text = "Level: " + charLevel;
        xp = minXP;
        lives = maxLives;
        healthBar.SetMaxHealth(maxLives);
        xpBar.SetMinXP(minXP);

    }


   
    // Update is called once per frame
    void Update()
    {
        Movement();
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
     public void Damage(int damageAmount)
    {
        lives = lives - damageAmount;
        healthBar.SetHealth(lives);

        if (lives <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void EarnXP(float xpAmount)
    {
        xp = xp + ((xpAmount / charLevel) * .5f);
        xpBar.SetXP(xp);

        if (xp == 1)
        {
            xp = minXP;
            xpBar.SetXP(xp);
            LevelUp();

        }
    }

    public void LevelUp()
    {

        if (xp == 1 && charLevel < 1)
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }
}
