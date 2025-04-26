using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public static class SaveData
{
    private static string path => Application.persistentDataPath + "/ProfileData.json";

    public static void SaveToJson(Profile profile)
    {
        string json = JsonUtility.ToJson(profile);
        File.WriteAllText(path, json);
    }

    public static Profile LoadFromJson()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<Profile>(json);
        }
        else
        {
            return new Profile();
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