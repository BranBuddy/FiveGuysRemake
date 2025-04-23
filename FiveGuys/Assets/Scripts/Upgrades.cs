using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrades : MonoBehaviour
{
    private int rngSeed;
    public int rngUpgrade1;
    public int rngUpgrade2;
    public int rngUpgrade3;

    public GameObject UpgradeScreen;

    public TMP_Text option1Text;
    public TMP_Text option2Text;


    public Button option1;
    public Button option2;


    private GameObject player;
    private PlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        rngSeed = Random.Range(0, 101);
        Debug.Log(rngSeed);
        Random.InitState(rngSeed);

        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerScript = player.GetComponent<PlayerScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Upgrade()
    {
        UpgradeScreen.SetActive(true);
        Time.timeScale = 0;
        rollUpgrade(option1Text, rngUpgrade1, option1);
        rollUpgrade(option2Text, rngUpgrade2, option2);

    }

    public void rollUpgrade(TMP_Text option, int rngUpgrade, Button UpgradeButton)
    {
        rngUpgrade = Random.Range(0, 101);
        if (rngUpgrade >= 0 && rngUpgrade < 21)
        {
            option.text = "Damage";
            playerScript.damageDealt *= 1.5f;
            UpgradeButton.onClick.AddListener(UpgradeDamage);
            Debug.Log(rngUpgrade);

        }
        if (rngUpgrade >= 21 && rngUpgrade < 41)
        {
            option.text = "Speed";
            playerScript.speed *= 1.5f;
            UpgradeButton.onClick.AddListener(UpgradeSpeed);
            Debug.Log(rngUpgrade);

        }
        if (rngUpgrade >= 41 && rngUpgrade < 61)
        {
            option.text = "Health";
            playerScript.lives += 1f;
            UpgradeButton.onClick.AddListener(UpgradeHealth);
            Debug.Log(rngUpgrade);

        }
        if (rngUpgrade >= 61 && rngUpgrade < 89)
        {
            option.text = "Fire Rate";
            GameObject.Find("Bullet(Clone)").GetComponent<BulletScript>().bulletSpeed += 1.5f;
            UpgradeButton.onClick.AddListener(UpgradeFireRate);
            Debug.Log(rngUpgrade);

        }
        if (rngUpgrade > 89)
        {
            option.text = "Glass Cannon";
            playerScript.lives *= .5f;
            playerScript.speed *= 2f;
            UpgradeButton.onClick.AddListener(UpgradeGlassCannon);
            Debug.Log(rngUpgrade);
        }
    }

    public void UpgradeDamage()
    {
        Debug.Log("Damage has been upgraded!");
        UpgradeScreen.SetActive(false);
        Time.timeScale = 1;
 
    }

    public void UpgradeHealth()
    {
        Debug.Log("Health has been upgraded!");
        UpgradeScreen.SetActive(false);
        Time.timeScale = 1;


    }

    public void UpgradeSpeed()
    {
        Debug.Log("Speed has been upgraded!");
        UpgradeScreen.SetActive(false);
        Time.timeScale = 1;


    }

    public void UpgradeGlassCannon()
    {
        Debug.Log("You now deal double damage while having reduced health!");
        UpgradeScreen.SetActive(false);
        Time.timeScale = 1;

    }

    public void UpgradeFireRate()
    {
        Debug.Log("Fire Rate has been upgraded!");
        UpgradeScreen.SetActive(false);
        Time.timeScale = 1;

    }


}
