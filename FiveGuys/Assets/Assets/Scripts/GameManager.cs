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
        SpawnEnemy();
        CameraFollow();
        GameObject.Find("PlayerHolder(Clone)").transform.GetChild(0).gameObject.SetActive(false);
    }

    private void Awake()
    {
        var cam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("GameManager").GetComponent<PlayerSelect>().gameStarted == true)
        {
            GameObject.Find("PlayerHolder(Clone)").transform.GetChild(0).gameObject.SetActive(true);
            
        }

        
    }

    public void CameraFollow()
    {
        cam.Follow = GameObject.Find("PlayerHolder(Clone)").transform.GetChild(0).gameObject.transform;
        cam.LookAt = GameObject.Find("PlayerHolder(Clone)").transform.GetChild(0).gameObject.transform;
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
