using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    [SerializeField] private int weaponType;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void whatWeaponAmI()
    {
        if (weaponType == 0)
        {

        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy" && weaponType == 0)
        {

            collision.gameObject.GetComponent<EnemyFollow>().enemyDamaged(1);
        }
    }
}
