using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public CinemachineVirtualCamera cam;

    

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
        SpawnEnemy();
        SpawnEnemy();
        CameraFollow();

    }

    private void Awake()
    {
        var cam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CameraFollow()
    {
        cam.Follow = FindAnyObjectByType<PlayerScript>().transform;
        cam.LookAt = FindAnyObjectByType<PlayerScript>().transform;
    }

    void SpawnPlayer()
    {
        Instantiate(player, transform.position, transform.rotation);
    }

    void SpawnEnemy()
    {
        Instantiate(enemy, new Vector3(0, 0, 30), transform.rotation);
    }

}
