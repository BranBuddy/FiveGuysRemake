using System.Collections;
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

    [Header("Movement")]
    public float baseSpeed = 3f;
    public float sprintMultiplier = 1.5f;
    private float currentSpeed;

    [Header("Player Stats")]
    public float lives;
    public float maxLives;
    public float sprint;
    public float maxSprint = 1f;
    public float sprintCost = 0.2f;
    public float chargeRate = 0.2f;

    [Header("XP and Level")]
    public float xp;
    public float minXP = 0f;
    public int charLevel = 1;

    [Header("Abilities")]
    public bool autoFire;
    public GameObject bulletPrefab;
    public int whatCharacterAmI;

    [Header("UI")]
    public Healthbar healthBar;
    public XPBar xpBar;
    public SprintBar sprintBar;
    public Image stamina;
    public TextMeshProUGUI levelUpText;

    [Header("Audio")]
    public AudioClip levelUpClip;
    public AudioClip bulletClip;

    private bool running;
    private Coroutine recharge;
    private PlayerScript playerScript;

    void Start()
    {
        currentSpeed = baseSpeed;
        lives = maxLives;
        sprint = maxSprint;
        autoFire = false;
        xp = minXP;
        charLevel = 1;

        healthBar.SetMaxHealth(maxLives);
        xpBar.SetMinXP(minXP);
        xpBar.SetXP(xp);
        sprintBar.SetSprint(sprint);
        levelUpText.text = "Level: " + charLevel;
    }

    void Update()
    {
        Movement();
        Sprinting();

        if (Input.GetKeyDown(KeyCode.Mouse0) && !autoFire)
        {
            ShootBullet();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            autoFire = !autoFire;

            if (autoFire)
                StartCoroutine(Autofire());
            else
                StopCoroutine(Autofire());
        }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        transform.Translate(direction * currentSpeed * Time.deltaTime);
    }

    void Sprinting()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && sprint > 0)
        {
            currentSpeed = baseSpeed * sprintMultiplier;
            running = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || sprint <= 0)
        {
            currentSpeed = baseSpeed;
            running = false;
        }

        if (running)
        {
            sprint -= sprintCost * Time.deltaTime;
            sprint = Mathf.Clamp(sprint, 0, maxSprint);
            sprintBar.SetSprint(sprint);

            if (recharge != null)
                StopCoroutine(recharge);

            recharge = StartCoroutine(RechargeSprint());
        }
    }

    private IEnumerator RechargeSprint()
    {
        yield return new WaitForSeconds(1f);

        while (sprint < maxSprint && !running)
        {
            sprint += chargeRate * Time.deltaTime;
            sprint = Mathf.Clamp(sprint, 0, maxSprint);
            sprintBar.SetSprint(sprint);
            yield return null;
        }
    }

    public void Damage(float damageAmount)
    {
        lives -= damageAmount;
        healthBar.SetHealth(lives);

        PlayerDamagedEvent?.Invoke(damageAmount);

        if (lives <= 0)
        {
            Destroy(gameObject);
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
            xp = 0;
            LevelUp();
            AudioSource.PlayClipAtPoint(levelUpClip, transform.position, 0.7f);
        }
    }

    private void LevelUp()
    {
        charLevel++;
        levelUpText.text = "Level: " + charLevel;
    }

    void ShootBullet()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(bulletClip, transform.position, 0.7f);
    }

    IEnumerator Autofire()
    {
        while (autoFire)
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
        baseSpeed += amount;
        currentSpeed = baseSpeed;
    }
}
