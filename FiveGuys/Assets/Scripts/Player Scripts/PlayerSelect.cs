using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    private PlayerScript player;
    private int whatCharacterAmI;
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
    }

    public void SelectPlayerTwo()
    {
        GameObject.Find("Player(Clone)").GetComponent<PlayerScript>().maxLives = 1;
        GameObject.Find("Player(Clone)").GetComponent<PlayerScript>().speed = 10;

    }

    public void SelectPlayerThree()
    {
        GameObject.Find("Player(Clone)").GetComponent<PlayerScript>().maxLives = 5;
        GameObject.Find("Player(Clone)").GetComponent<PlayerScript>().speed = 5;
    }
}
