using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI treasureText;
    public GameObject gameOverUI;
    public GameObject playerController;

    private int treasureCount = 0;

    public AudioSource audioSource; // Reference to AudioSource

    public AudioClip damageSound;    // Sound for taking damage
    public AudioClip treasureSound;  // Sound for collecting treasure
    public AudioClip gameOverSound;  // Sound for game over

    private void Start()
    {
        currentHealth = maxHealth;
        treasureCount = 0;

        UpdateHealthText();
        UpdateTreasureText();

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth != 0)
            currentHealth -= damage;
        
        Debug.Log($"Player Health: {currentHealth}");

        // Play damage sound effect
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        UpdateHealthText();

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    public void AddTreasure(int amount)
    {
        treasureCount += amount;
        UpdateTreasureText();

        Debug.Log($"Treasures Collected: {treasureCount}");

        // Play treasure pickup sound
        if (audioSource != null && treasureSound != null)
        {
            audioSource.PlayOneShot(treasureSound);
        }
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

        // Stop background music
        BackgroundMusic.StopMusic();

        // Play game over sound
        if (audioSource != null && gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        if (playerController != null)
        {
            playerController.SetActive(false);
        }

        Invoke("RestartGame", 8f);  
    }

    private void RestartGame()
    {
        BackgroundMusic.StartMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int GetTreasureCount()
    {
        return treasureCount;
    }
}
