using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;

public class Player : MonoBehaviour
{
     private float horizontalInput;
     private float verticalInput;
    public float speed = 5f;
    public float lives = 3f;
    public float maxLives = 3f;
    public float minXP = 0f;
    public float maxSprint = 1f;
    public float sprint;
    public float xp;
    public bool autoFire = false;
    public GameObject enemy;
    public GameObject bullet;
    private int charLevel;
    public float sprintCost;

    public Healthbar healthBar;
    public XPBar xpBar;
    public SprintBar sprintBar;

    public Image stamina;


    public Vector2 turn;

    public TextMeshProUGUI levelUpText;

    public bool running;

    private Coroutine recharge;
    public float chargeRate;

    void Start()
    {
        charLevel = 1;
        levelUpText.text = "Level: " + charLevel;
        xp = minXP;
        lives = maxLives;

        healthBar.SetMaxHealth(maxLives);
        xpBar.SetMinXP(minXP);
        sprintBar.SetSprint(sprint);

    }
   
    // Update is called once per frame
    void Update()
    {
        Movement();

        Sprinting();

        if (running == true)
        {
            sprint -= sprintCost * Time.deltaTime;

            if (sprint == 0)
            {
                sprint = 0;
            }

            sprintBar.SetSprint(sprint);

           if(recharge != null)
            {
                StopCoroutine(recharge);
            }
           recharge = StartCoroutine(RechargeSprint());

        }

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
    }
    
    //moves player
    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * Time.deltaTime * speed);

    }

    void Sprinting()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= 1.5f;

            sprint -= sprintCost;

            running = true;
            Debug.Log(sprint);

            sprintBar.SetSprint(sprint);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || sprint <= 0)
        {
            speed = 6f;
            running = false;

        }


    }

    private IEnumerator RechargeSprint()
    {
        yield return new WaitForSeconds(1f);
        while (sprint < maxSprint)
        {
            sprint += chargeRate / 10f;
            if (sprint > maxSprint)
            {
                sprint = maxSprint;
            }
            sprintBar.SetSprint(sprint);
            yield return new WaitForSeconds(.1f);
        }
    }

    //does damage to player in 1/3 increments
    public void Damage(float damageAmount)
    {
        lives -= damageAmount;
        healthBar.SetHealth(lives);

        if (lives <= 0)
        {
            
            Destroy(this.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }

    public void EarnXP(float xpAmount)
    {
        xp = xp + ((xpAmount / charLevel) * .5f);
        xpBar.SetXP(xp);

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
    public void HealPlayer(int healthGained)
    {
        if (lives < maxLives)
        {
            lives += healthGained;
            healthBar.SetHealth(lives);
        }
    }
}
