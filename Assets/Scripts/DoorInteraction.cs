using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    // Public variables for controlling the door's behavior and UI elements
    public Transform door; // The door object to slide
    public Vector3 openPosition; // The local position of the door when open
    public Vector3 closedPosition; // The local position of the door when closed
    public float slideSpeed = 2f; // Speed at which the door slides
    public GameObject openDoorPrompt; // UI prompt to open the door
    public GameObject closeDoorPrompt; // UI prompt to close the door
    
    private bool isPlayerNearby = false; // To track if the player is near the door
    private bool isOpen = false; // To track if the door is open or closed
    private Vector3 targetPosition; // The current target position of the door (open or closed)

    public AudioSource audioSource; // Reference to AudioSource for sound effects
    public AudioClip doorInteractionSound; // Sound to play when interacting with the door

    private void Start()
    {
        // Initialize the target position to the closed position at the start
        targetPosition = closedPosition;

        // Hide the interaction prompts initially
        if (openDoorPrompt != null || closeDoorPrompt != null)
        {
            openDoorPrompt.SetActive(false);
            closeDoorPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        // Check if the player is nearby and presses the 'F' key to interact with the door
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            // Toggle the door state (open/closed) when the player interacts with it
            isOpen = !isOpen;
            targetPosition = isOpen ? openPosition : closedPosition;

            // Play the door interaction sound if available
            if (audioSource != null && doorInteractionSound != null)
            {
                audioSource.PlayOneShot(doorInteractionSound);
            }
        }

        // Smoothly move the door toward the target position (open or closed)
        if (door != null)
        {
            door.localPosition = Vector3.MoveTowards(door.localPosition, targetPosition, Time.deltaTime * slideSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the trigger zone (door area)
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true; // Player is in range of the door

            // Show the appropriate interaction prompt based on the door's state
            if (openDoorPrompt != null || closeDoorPrompt != null)
            {
                if (targetPosition == closedPosition)
                    openDoorPrompt.SetActive(true); // Show prompt to open the door
                else if (targetPosition == openPosition)
                    closeDoorPrompt.SetActive(true); // Show prompt to close the door
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exits the trigger zone (door area)
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false; // Player is no longer near the door

            // Hide the interaction prompts when the player leaves the area
            if (openDoorPrompt != null || closeDoorPrompt != null)
            {
                openDoorPrompt.SetActive(false);
                closeDoorPrompt.SetActive(false);
            }
        }
    }
}
