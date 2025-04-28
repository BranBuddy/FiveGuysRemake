using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    private PlayerScript player;
    private int whatCharacterAmI;
    public GameObject player_;

    private int playerTwoUnlock = 500;
    private int playerThreeUnlock = 1000;
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectPlayerOne()
    {
        GameObject.Find("Player(Clone)").GetComponent<PlayerScript>().maxLives = 3;
        GameObject.Find("Player(Clone)").GetComponent<PlayerScript>().speed = 7;
        GameObject.Find("Player(Clone)").GetComponent<PlayerScript>().meshRenderer.material = GameObject.Find("Player(Clone)").GetComponent<PlayerScript>().materials[0];
    }

    public void SelectPlayerTwo()
    {
        if (SaveManager.instance.profile.kills >= playerTwoUnlock)
        {
            GameObject.Find("Player(Clone)").GetComponent<PlayerScript>().maxLives = 1;
            GameObject.Find("Player(Clone)").GetComponent<PlayerScript>().speed = 10;
            GameObject.Find("Player(Clone)").GetComponent<PlayerScript>().meshRenderer.material = GameObject.Find("Player(Clone)").GetComponent<PlayerScript>().materials[1];
        }
        else Debug.LogWarning("Player not Unlocked");

    }

    public void SelectPlayerThree()
    {
        if (SaveManager.instance.profile.kills >= playerThreeUnlock)
        {
            GameObject.Find("Player(Clone)").GetComponent<PlayerScript>().maxLives = 5;
            GameObject.Find("Player(Clone)").GetComponent<PlayerScript>().speed = 5;
            GameObject.Find("Player(Clone)").GetComponent<PlayerScript>().meshRenderer.material = GameObject.Find("Player(Clone)").GetComponent<PlayerScript>().materials[2];
        }
        else Debug.LogWarning("Player not Unlocked");
    }

    public void StartGame()
    {
        GameObject.Find("Player(Clone)").transform.GetChild(0).gameObject.SetActive(true);

    }
}
