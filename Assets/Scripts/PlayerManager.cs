using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    // Player health variables
    public int maxHealth = 3; // Maximum health of the player
    private int currentHealth; // Current health of the player

    private int treasureCount = 0; // Counter for collected treasures

    // UI Elements
    public TextMeshProUGUI healthText; // UI text to display health
    public TextMeshProUGUI treasureText; // UI text to display treasure count
    public GameObject gameOverUI; // UI element for Game Over screen
    public GameObject playerController; // Reference to the player controller script or object

    // Audio components
    public AudioSource audioSource; // Audio source for playing sounds
    public AudioClip damageSound; // Sound effect for taking damage
    public AudioClip treasureSound; // Sound effect for collecting treasure
    public AudioClip gameOverSound; // Sound effect for game over

    private void Start()
    {
        // Initialize health and treasure count
        currentHealth = maxHealth;
        treasureCount = 0;

        // Update UI text
        UpdateHealthText();
        UpdateTreasureText();

        // Hide Game Over screen at the start
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    // Method to handle taking damage
    public void TakeDamage(int damage)
    {
        if (currentHealth != 0) 
            currentHealth -= damage; // Reduce player's health

        // Play damage sound effect
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        // Update UI
        UpdateHealthText();

        // Check if health reaches zero
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    // Method to handle collecting treasures
    public void AddTreasure(int amount)
    {
        treasureCount += amount; // Increase treasure count
        UpdateTreasureText(); // Update UI

        // Play treasure pickup sound
        if (audioSource != null && treasureSound != null)
        {
            audioSource.PlayOneShot(treasureSound);
        }
    }

    // Updates the health UI text
    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {currentHealth}";
        }
    }

    // Updates the treasure UI text
    private void UpdateTreasureText()
    {
        if (treasureText != null)
        {
            treasureText.text = $"Treasures: {treasureCount}";
        }
    }

    // Handles game over logic
    private void GameOver()
    {
        // Stop background music
        BackgroundMusic.StopMusic();

        // Play game over sound effect
        if (audioSource != null && gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }

        // Show Game Over UI
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        // Disable player controls
        if (playerController != null)
        {
            playerController.SetActive(false);
        }

        // Restart the game after 8 seconds
        Invoke("RestartGame", 8f);  
    }

    // Restarts the game by reloading the current scene
    private void RestartGame()
    {
        BackgroundMusic.StartMusic(); // Restart background music
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload scene
    }

    // Returns the current number of collected treasures
    public int GetTreasureCount()
    {
        return treasureCount;
    }
}
