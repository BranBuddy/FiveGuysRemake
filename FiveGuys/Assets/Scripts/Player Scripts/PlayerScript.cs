using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public delegate void OnPlayerDamaged(float damageAmount);
    public static event OnPlayerDamaged PlayerDamagedEvent;

    private float horizontalInput;
    private float verticalInput;

    public float speed;
    public float lives;
    public float maxLives;
    public float minXP = 0f;
    public float maxSprint = 1f;

    public float sprint;
    public float xp;

    public bool autoFire;
    public GameObject bulletPrefab;

    public int charLevel;
    public float sprintCost;

    public int whatCharacterAmI;

    public AudioClip levelUpClip;
    public AudioClip bulletClip;

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
        speed = 3;
        maxLives = 2;
        charLevel = 1;
        levelUpText.text = "Level: " + charLevel;
        xp = minXP;
        lives = maxLives;
        autoFire = false;

        healthBar.SetMaxHealth(maxLives);
        xpBar.SetMinXP(minXP);
        sprintBar.SetSprint(sprint);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Sprinting();

        if (running)
        {
            sprint -= sprintCost * Time.deltaTime;

            if (sprint == 0)
            {
                sprint = 0;
            }

            sprintBar.SetSprint(sprint);

            if (recharge != null)
            {
                StopCoroutine(recharge);
            }
            recharge = StartCoroutine(RechargeSprint());
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !autoFire)
        {
            ShootbulletPrefab();
        }
        else if (Input.GetKeyDown(KeyCode.E) && !autoFire)
        {
            autoFire = true;
            StartCoroutine("Autofire");
        }
        else if (Input.GetKeyDown(KeyCode.E) && autoFire)
        {
            autoFire = false;
            StopCoroutine("Autofire");
        }
    }

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
            sprintBar.SetSprint(sprint);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || sprint <= 0)
        {
            speed *= 0.5f;
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
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Event
    public void Damage(float damageAmount)
    {
        lives -= damageAmount;
        healthBar.SetHealth(lives);

        PlayerDamagedEvent?.Invoke(damageAmount);

        if (lives <= 0)
        {
            Destroy(this.gameObject);
            StartCoroutine(WaitForDeath());
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void EarnXP(float xpAmount)
    {
        xp += (xpAmount / charLevel) * 0.5f;
        xpBar.SetXP(xp);

        if (xp >= 1)
        {
            xp = minXP;
            xpBar.SetXP(xp);
            LevelUp();
            AudioSource.PlayClipAtPoint(levelUpClip, transform.position, 0.7f);
        }
    }

    public void LevelUp()
    {
        charLevel++;
        levelUpText.text = "Level: " + charLevel;
    }

    void ShootbulletPrefab()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(bulletClip, transform.position, 0.7f);
    }

    IEnumerator Autofire()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

        while (enemy != null)
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(bulletClip, transform.position, 0.7f);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(3f);
    }

    public void HealPlayer(int healthGained)
    {
        if (lives < maxLives)
        {
            lives += healthGained;
            healthBar.SetHealth(lives);
        }
    }

    public void ChangeSpeed(int amount)
    {
        speed += amount;
    }
}

