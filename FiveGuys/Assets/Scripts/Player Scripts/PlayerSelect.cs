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
        player = GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WhatPlayerAmI()
    {
        if (whatCharacterAmI == 0)
        {
            player.maxLives = 3;
            player.speed = 5f;
        }
        else if (whatCharacterAmI == 1)
        {
            player.maxLives = 2;
            GameObject.Find("Player(Clone)").GetComponent<PlayerScript>().ChangeSpeed(7);
            
        }
        else if (whatCharacterAmI == 2)
        {
            player.maxLives = 5;
            player.speed = 3f;
        }

    }

    public void SelectPlayerOne()
    {
        whatCharacterAmI = 0;
        Debug.Log(whatCharacterAmI);
    }

    public void SelectPlayerTwo()
    {
        whatCharacterAmI = 1;
        Debug.Log(whatCharacterAmI);
        
    }

    public void SelectPlayerThree()
    {
        whatCharacterAmI = 2;
        Debug.Log(whatCharacterAmI);
    }
}
