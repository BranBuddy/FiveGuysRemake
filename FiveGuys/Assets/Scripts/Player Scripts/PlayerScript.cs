using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.ProBuilder;

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

    internal float damageDealt;

    internal MeshRenderer meshRenderer;
    public Material[] materials;

    public float sprint;
    public float xp;

    public bool autoFire;
    private BulletPool bulletPool;

    public GameObject rocketPrefab;
    private float rocketCooldown = 1f;

    public int charLevel;
    public float sprintCost;

    public int whatCharacterAmI;

    public AudioClip levelUpClip;
    public AudioClip bulletClip;

    private Animator animator;

    public HealthBar1 healthBar;
    public XPBar1 xpBar;
    public SprintBar sprintBar;

    public Image stamina;

    public Vector2 turn;

    public TextMeshProUGUI levelUpText;

    private float rotationSpeed;

    public bool running;
    public bool leveledUp;

    private Coroutine recharge;
    public float chargeRate;

    void Start()
    {
        speed = 3;
        maxLives = 2;
        charLevel = 1;
        levelUpText.text = "Level: " + charLevel;
        xp = minXP;
        damageDealt = 1;
        lives = maxLives;
        autoFire = false;
        leveledUp = false;

        rotationSpeed = 5;

        meshRenderer = this.transform.GetComponent<MeshRenderer>();

        meshRenderer.material = materials[0];

        animator = GetComponent<Animator>();

        healthBar.SetMaxHealth(maxLives);
        xpBar.SetMinXP(minXP);
        sprintBar.SetSprint(sprint);

        bulletPool = GameObject.Find("GameManager").GetComponent<BulletPool>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Sprinting();

        Debug.Log(xp);

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
        { // shoot bullet manual
            ShootbulletPrefab();
        }
        else if (Input.GetKeyDown(KeyCode.E) && !autoFire)
        { // start auto shooting bullets
            autoFire = true;
            StartCoroutine("Autofire");
        }
        else if (Input.GetKeyDown(KeyCode.E) && autoFire)
        { // stop auto shooting bullets
            autoFire = false;
            StopCoroutine("Autofire");
        }

        if (Input.GetKeyDown(KeyCode.Z) && rocketCooldown <= 0f)
        { // Shoot rocket and reset cooldown
            ShootRocketPrefab();
            rocketCooldown = 1f;
        }
        else if (rocketCooldown > 0f)
        { // reduce cooldown
            rocketCooldown -= Time.deltaTime;
        }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * Time.deltaTime * speed;
        transform.Translate(movement);

        if(movement == Vector3.zero)
        {
            animator.SetFloat("Speed", 0);
        }
        else
        {
            animator.SetFloat("Speed", 1);
        }

        
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
            GameObject.Find("Canvas").GetComponent<Upgrades>().Upgrade();
            AudioSource.PlayClipAtPoint(levelUpClip, transform.position, 0.7f);
        }
    }

    public void LevelUp()
    {
        charLevel++;
        levelUpText.text = "Level: " + charLevel;
    }

    

    void ShootbulletPrefab()
    { // Creates bullet and plays sound
        GameObject bullet = bulletPool.GetObject();
        bullet.transform.position = transform.position;
        AudioSource.PlayClipAtPoint(bulletClip, transform.position, 0.7f);
    }

    void ShootRocketPrefab()
    { // Creates rocket
        Instantiate(rocketPrefab, transform.position, transform.rotation);
    }

    IEnumerator Autofire()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

        while (true && enemy != null)

        {
            GameObject bullet = bulletPool.GetObject();
            bullet.transform.position = transform.position;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Freezer"))
        {
            speed -= 5;
        }
        if (other.CompareTag("Puddle"))
        {
            speed += 5;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Freezer"))
        {
            speed += 5;
        }
        if (other.CompareTag("Puddle"))
        {
            speed -= 5;
        }
    }

}

