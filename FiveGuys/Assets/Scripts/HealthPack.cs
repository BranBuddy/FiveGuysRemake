using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public GameObject health;

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
        if (other.tag == "Player")
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerScript>().HealPlayer(1);
            StartCoroutine(respawnItem());

        }
    }

    private IEnumerator respawnItem()
    {
        health.SetActive(false);
        yield return new WaitForSeconds(5.0f);
        health.SetActive(true);


    }

}
