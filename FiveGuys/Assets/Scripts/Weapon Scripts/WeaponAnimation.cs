using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
    
    public GameObject weapon;
    public bool CanAttack = true;
    public float attackCooldown = .5f;
    public GameObject weaponHolder;
    private Animator animator;

    void Start()
    {
        animator = GameObject.Find("Player(Clone)").GetComponent<Animator>();
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
        animator.SetTrigger("Attack");
        StartCoroutine(ResetCooldown());

    }

    IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        Animator anim = weapon.GetComponent<Animator>();
        CanAttack = true;
        animator.ResetTrigger("Attack");
        anim.ResetTrigger("Attack");
        weapon.gameObject.SetActive(false);
    }

    

}
