using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    [SerializeField] private WeaponType weaponType = WeaponType.Melee;

    public enum WeaponType
    {
        Melee,
        Ranged,    // Add more types as needed
        Magic
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyFollow enemy = other.GetComponent<EnemyFollow>();
            if (enemy != null)
            {
                switch (weaponType)
                {
                    case WeaponType.Melee:
                        enemy.TakeDamage(1);
                        break;

                    case WeaponType.Ranged:
                        enemy.TakeDamage(2);
                        break;

                    case WeaponType.Magic:
                        enemy.TakeDamage(3); // Example
                        break;

                        // Add more types if needed
                }
            }
        }
    }
}
