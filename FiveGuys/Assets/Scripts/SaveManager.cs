using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public Profile profile;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            profile = SaveData.LoadFromJson();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    private void OnApplicationQuit()
    {
        SaveData.SaveToJson(profile);
    }
}
