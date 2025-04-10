using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentUpgrades : MonoBehaviour
{
    private int upgradeCoins = 0;
    public int damageAdded = 0;
    public int healthAdded = 0;
    public int expGainAdded = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadUpgrades(int coins, int damage, int health, int exp)
    {
        upgradeCoins = coins;
        damageAdded = damage;
        healthAdded = health;
        expGainAdded = exp;
    }

    void gainCoins(int roundsSurvived)
    {
        upgradeCoins += roundsSurvived;
    }

    void UpgradeDamage()
    {
        if (upgradeCoins > 0)
        {
            upgradeCoins -= 1;
            damageAdded += 1;
        }
    }

    void UpgradeHealth()
    {
        if (upgradeCoins > 0)
        {
            upgradeCoins -= 1;
            healthAdded += 1;
        }
    }

    void UpgradeExpGain()
    {
        if (upgradeCoins > 0)
        {
            upgradeCoins -= 1;
            expGainAdded += 1;
        }
    }
}

