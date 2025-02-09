using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class TreasureInteraction : MonoBehaviour
{
    public GameObject goldenDragon; // The treasure to disappear
    public GameObject interactionPrompt; // UI prompt to display "Press F to collect"
    public int treasureValue = 1; // Value of the treasure (e.g., 1 point)
    public TextMeshProUGUI treasureCounterText; // UI Text for displaying "Treasures: X"
    private bool isPlayerNearby = false; // To track if the player is in range

    private void Start()
    {
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false); // Hide the interaction prompt initially
        }

        // Initialize the treasure counter
        UpdateTreasureCounter();
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            CollectTreasure();
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

    private void CollectTreasure()
    {
        // Deactivate the treasure (e.g., make it disappear)
        if (goldenDragon != null)
        {
            goldenDragon.SetActive(false);
        }

        // Update the treasure count in the Game Manager
        GameManager.Instance.AddTreasure(treasureValue);
        FindObjectOfType<PlayerHealth>().AddTreasure(1);


        // Update the treasure counter UI
        UpdateTreasureCounter();

        // Optionally, hide the interaction prompt
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }

        Debug.Log("Treasure collected! Current treasure count: " + GameManager.Instance.GetTreasureCount());

        // Disable further interaction with this treasure
        Destroy(this);
    }

    private void UpdateTreasureCounter()
    {
        if (treasureCounterText != null)
        {
            treasureCounterText.text = "Treasures: " + GameManager.Instance.GetTreasureCount();
        }
    }
}
