using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using UnityEngine.SceneManagement; // For restarting the scene

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // Maximum health
    private int currentHealth;

    public TextMeshProUGUI healthText; // UI text to display health
    public TextMeshProUGUI treasureText; // UI text to display treasures
    public GameObject gameOverUI; // UI element for the Game Over screen
    public GameObject playerController; // Reference to the player controller script or object

    private int treasureCount = 0; // Counter for the treasures

    private void Start()
    {
        // Initialize health
        currentHealth = maxHealth;

        // Initialize treasure count
        treasureCount = 0;

        // Update the UI displays
        UpdateHealthText();
        UpdateTreasureText();

        // Ensure the Game Over UI is hidden
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        // Reduce health
        if (currentHealth != 0)
            currentHealth -= damage;
        
        Debug.Log($"Player Health: {currentHealth}");

        // Update the health display
        UpdateHealthText();

        // Check if the player is out of health
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    public void AddTreasure(int amount)
    {
        // Increase the treasure count
        treasureCount += amount;

        // Update the treasure display
        UpdateTreasureText();

        Debug.Log($"Treasures Collected: {treasureCount}");
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {currentHealth}";
        }
    }

    private void UpdateTreasureText()
    {
        if (treasureText != null)
        {
            treasureText.text = $"Treasures: {treasureCount}";
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");

        // Show the Game Over UI
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        // Disable the player's control
        if (playerController != null)
        {
            playerController.SetActive(false); // Disable the player object or script
        }

        // Stop the game or reload the scene after a delay
        Invoke("RestartGame", 3f); // Reloads the scene after 3 seconds
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the current scene
    }

    public int GetTreasureCount()
    {
        return treasureCount;
    }
}