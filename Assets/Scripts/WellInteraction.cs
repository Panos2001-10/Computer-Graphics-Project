using UnityEngine;
using UnityEngine.SceneManagement; // For restarting the scene

public class WellInteraction : MonoBehaviour
{
    public GameObject interactionPrompt; // UI prompt to display "Press F to win"
    public GameObject winningText; // UI element for the winning text
    private bool isPlayerNearby = false; // To track if the player is in range
    public GameObject playerController; // Reference to the player controller script or object

    public AudioSource audioSource; // Reference to AudioSource
    public AudioClip winSound; // Sound to play when winning

    private void Start()
    {
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false); // Hide the interaction prompt initially
        }

        if (winningText != null)
        {
            winningText.SetActive(false); // Hide the winning text initially
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            if (GameManager.Instance.GetTreasureCount() >= 2) // Check if the player has both treasures
            {
                WinGame();
            }
            else
            {
                Debug.Log("You need to collect both treasures to win!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(true); // Show the prompt when the player is nearby
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(false); // Hide the prompt when the player leaves
            }
        }
    }

    private void WinGame()
    {
        if (winningText != null)
        {
            winningText.SetActive(true); // Show the winning text
        }

        // Stop background music
        BackgroundMusic.StopMusic();

        // Play win sound effect
        if (audioSource != null && winSound != null)
        {
            audioSource.PlayOneShot(winSound);
        }

        // Disable the player's control
        if (playerController != null)
        {
            playerController.SetActive(false); // Disable the player object or script
        }

        // Stop the game or reload the scene after a delay
        Invoke("RestartGame", 8f); // Reloads the scene after 8 seconds
    }

    private void RestartGame()
    {
        BackgroundMusic.StartMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the current scene
    }
}
