using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
    
    public GameObject weapon;
    public bool CanAttack = true;
    public float attackCooldown = .5f;
    public GameObject weaponHolder;

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
               weapon.gameObject.SetActive(true);
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
        weapon.gameObject.SetActive(false);
    }

    

}
