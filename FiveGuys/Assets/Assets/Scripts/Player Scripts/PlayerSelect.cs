using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    internal PlayerScript player;
    internal int whatCharacterAmI;
    internal bool gameStarted = false;
    void Start()
    {
        player = GetComponent<PlayerScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void StartGame()
    {
        gameStarted = true;
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
