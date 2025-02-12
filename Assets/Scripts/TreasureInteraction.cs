using UnityEngine;
using TMPro; // Import TextMeshPro namespace for handling UI text

public class TreasureInteraction : MonoBehaviour
{
    // Public variables to reference the treasure and UI elements
    public GameObject goldenDragon; // The treasure object (e.g., golden dragon)
    public GameObject interactionPrompt; // UI prompt to display "Press F to collect"
    public int treasureValue = 1; // Value of the treasure (e.g., 1 point for collection)
    public TextMeshProUGUI treasureCounterText; // UI Text to display the treasure count
    private bool isPlayerNearby = false; // To track if the player is in range of the treasure

    private void Start()
    {
        // Hide the interaction prompt initially
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }

        // Initialize the treasure counter UI with the current treasure count
        UpdateTreasureCounter();
    }

    private void Update()
    {
        // Check if the player is nearby and presses the 'F' key to collect the treasure
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            CollectTreasure(); // Collect the treasure when the player interacts
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the trigger zone
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true; // Mark that the player is nearby
            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(true); // Show the interaction prompt to the player
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player has left the trigger zone
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false; // Mark that the player is no longer nearby
            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(false); // Hide the interaction prompt when the player leaves
            }
        }
    }

    private void CollectTreasure()
    {
        // Deactivate the treasure object (e.g., make it disappear)
        if (goldenDragon != null)
        {
            goldenDragon.SetActive(false);
        }

        // Update the treasure count in the Game Manager and Player Manager
        GameManager.Instance.AddTreasure(treasureValue);
        FindObjectOfType<PlayerManager>().AddTreasure(1);

        // Update the UI treasure counter
        UpdateTreasureCounter();

        // Optionally hide the interaction prompt after collection
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }

        // Disable further interaction with this treasure object
        Destroy(this);
    }

    private void UpdateTreasureCounter()
    {
        // Update the treasure counter UI text with the current treasure count
        if (treasureCounterText != null)
        {
            treasureCounterText.text = "Treasures: " + GameManager.Instance.GetTreasureCount();
        }
    }
}
