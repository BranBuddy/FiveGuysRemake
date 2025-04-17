using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public Profile profile = new Profile();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SaveToJson();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            LoadFromJson();
            
        }
    }

    public void SaveToJson()
    {
        string profileData = JsonUtility.ToJson(profile);
        string filePath = Application.persistentDataPath + "/ProfileData.json";
        Debug.Log(filePath);

        System.IO.File.WriteAllText(filePath, profileData);
        Debug.Log("Created");
    }

    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/ProfileData.json";
        string profileData = System.IO.File.ReadAllText(filePath);

        profile = JsonUtility.FromJson<Profile>(profileData);
        Debug.Log("Loaded");
    }
}

[System.Serializable]
public class Profile
{
    

    public string playerName;
    public string characterName;

    

}