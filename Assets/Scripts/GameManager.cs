using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance of GameManager to ensure only one instance exists
    public static GameManager Instance;

    private int treasureCount = 0; // Tracks the number of treasures collected

    private void Awake()
    {
        // Ensure there's only one instance of the GameManager across scenes
        if (Instance == null)
        {
            Instance = this; // Set this instance as the singleton
            DontDestroyOnLoad(gameObject); // Keep this object across scene transitions
        }
        else
        {
            Destroy(gameObject); // Destroy this object if an instance already exists
        }
    }

    // Method to add treasure to the total count
    public void AddTreasure(int amount)
    {
        treasureCount += amount; // Increase the treasure count by the specified amount
    }

    // Method to get the current treasure count
    public int GetTreasureCount()
    {
        return treasureCount; // Return the current treasure count
    }
}
