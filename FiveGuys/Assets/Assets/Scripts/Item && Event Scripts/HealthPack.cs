using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public GameObject health;
    public AudioClip healthClip;
    private bool canGetHealth = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && canGetHealth == true)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerScript>().HealPlayer(1);
            AudioSource.PlayClipAtPoint(healthClip, transform.position, .7f);
            StartCoroutine(respawnItem());

        }
    }

    private IEnumerator respawnItem()
    {
        health.SetActive(false);
        canGetHealth = false;
        yield return new WaitForSeconds(5.0f);
        canGetHealth = true;
        health.SetActive(true);


    }

}
