using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public Profile profile = new Profile();
    private string path;

    private void Awake()
    {
        path = Application.persistentDataPath + "/ProfileData.json";
    }

    private void Start()
    {
        LoadFromJson();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SaveToJson();
        }
    }

    public void SaveToJson()
    {
        string profileData = JsonUtility.ToJson(profile);
        string filePath = Application.persistentDataPath + "/ProfileData.json";
        Debug.Log(filePath);

        File.WriteAllText(filePath, profileData);
        Debug.Log("Created");
    }

    public void LoadFromJson()
    {
        if (!File.Exists(path))
        {
            string profileData = File.ReadAllText(path);
            profile = JsonUtility.FromJson<Profile>(profileData);
            GetComponent<PermanentUpgrades>().LoadUpgrades(profile.coins, profile.damageUpgrades, profile.healthUpgrades, profile.expGainUpgrades);
            Debug.Log("Loaded");
        }
    }
}

[System.Serializable]
public class Profile
{
    public int kills = 0;
    public int coins = 0;
    public int healthUpgrades = 0;
    public int damageUpgrades = 0;
    public int expGainUpgrades = 0;
}