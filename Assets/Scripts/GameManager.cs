using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance

    private int treasureCount = 0;

    private void Awake()
    {
        // Ensure there's only one GameManager instance
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

    public void AddTreasure(int amount)
    {
        treasureCount += amount;
    }

    public int GetTreasureCount()
    {
        return treasureCount;
    }
}
