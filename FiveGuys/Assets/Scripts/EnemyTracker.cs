using UnityEngine;
using UnityEngine.UI;

public class EnemyTracker : MonoBehaviour
{
    public static EnemyTracker Instance;

    public int totalEnemiesSpawned = 0;
    public Text enemyCountText; 

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnemySpawned()
    {
        totalEnemiesSpawned++;
    }

    public void ShowFinalEnemyCount()
    {
        if (enemyCountText != null)
        {
            enemyCountText.text = "Enemies Spawned: " + totalEnemiesSpawned.ToString();
        }
        else
        {
            Debug.LogWarning("Enemy Count Text is not assigned!");
        }
    }
}