using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int weaponType;
    public GameObject weapon;
    public bool CanAttack = true;
    public float attackCooldown = .5f;
    public string enemyTag = "Enemy";


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

        if(Input.GetKeyDown(KeyCode.Mouse1)) {
            
                
                Debug.Log("pressed");
                if (CanAttack)
                {
                    weapon.SetActive(true);
                    Attack();
                    
                }
            }

        
        
    }

    public void Attack()
    {
        
        CanAttack = false;
        Animator anim = weapon.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetCooldown());

    }

    IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        CanAttack = true;
    }

    void whatWeaponAmI()
    {
        if (weaponType == 0)
        {

        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == enemyTag && weaponType == 0)
        {

            collision.gameObject.GetComponent<EnemyFollow>().enemyDamaged(1);
        }
    }

}
