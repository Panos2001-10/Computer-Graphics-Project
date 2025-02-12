using UnityEngine;
using UnityEngine.SceneManagement; // For restarting the scene

public class WellInteraction : MonoBehaviour
{
    // Public variables to reference UI elements and other components
    public GameObject interactionPrompt; // UI prompt to display "Press F to win"
    public GameObject winningText; // UI element for the winning text
    private bool isPlayerNearby = false; // To track if the player is in range of the well
    public GameObject playerController; // Reference to the player controller script or object

    public AudioSource audioSource; // Reference to AudioSource for playing sound
    public AudioClip winSound; // Sound to play when the player wins

    private void Start()
    {
        // Hide the interaction prompt initially when the game starts
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }

        // Hide the winning text initially when the game starts
        if (winningText != null)
        {
            winningText.SetActive(false);
        }
    }

    private void Update()
    {
        // Check if the player is nearby and presses the 'F' key to win
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            // Check if the player has collected both treasures
            if (GameManager.Instance.GetTreasureCount() >= 2)
            {
                WinGame(); // Trigger the win sequence if both treasures are collected
            }
            else
            {
                // Optionally, you can display a message or visual cue here, but Debug.Log is removed
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // When the player enters the trigger zone, mark the player as nearby
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true; // Player is within range of the well
            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(true); // Show the interaction prompt to the player
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When the player leaves the trigger zone, hide the interaction prompt
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false; // Player is no longer within range
            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(false); // Hide the prompt when the player leaves
            }
        }
    }

    private void WinGame()
    {
        // Show the winning text when the player wins the game
        if (winningText != null)
        {
            winningText.SetActive(true);
        }

        // Stop background music when the player wins
        BackgroundMusic.StopMusic();

        // Play the win sound effect
        if (audioSource != null && winSound != null)
        {
            audioSource.PlayOneShot(winSound);
        }

        // Disable the player's control (either by deactivating the player object or script)
        if (playerController != null)
        {
            playerController.SetActive(false);
        }

        // Restart the game after a delay (8 seconds)
        Invoke("RestartGame", 8f);
    }

    private void RestartGame()
    {
        // Start the background music again when restarting the game
        BackgroundMusic.StartMusic();

        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
