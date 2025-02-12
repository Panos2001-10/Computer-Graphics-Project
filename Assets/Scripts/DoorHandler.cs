using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public Transform door; // The door object to slide
    public Vector3 openPosition; // The local position of the door when open
    public Vector3 closedPosition; // The local position of the door when closed
    public float slideSpeed = 2f; // Speed at which the door slides
    public GameObject openDoorPrompt;
    public GameObject closeDoorPrompt;
    
    private bool isPlayerNearby = false;
    private bool isOpen = false;
    private Vector3 targetPosition;

    public AudioSource audioSource; // Reference to AudioSource
    public AudioClip doorInteractionSound; // Sound to play when interacting

    private void Start()
    {
        // Initialize the target position to the closed position
        targetPosition = closedPosition;
        if (openDoorPrompt != null || closeDoorPrompt !=null)
        {
            openDoorPrompt.SetActive(false);
            closeDoorPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            isOpen = !isOpen;
            targetPosition = isOpen ? openPosition : closedPosition;

            if (audioSource != null && doorInteractionSound != null)
            {
                audioSource.PlayOneShot(doorInteractionSound);
            }

        }

        // Smoothly move the door toward the target position
        if (door != null)
        {
            door.localPosition = Vector3.MoveTowards(door.localPosition, targetPosition, Time.deltaTime * slideSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the trigger zone
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;

            // Show the interaction prompt
            if (openDoorPrompt != null || closeDoorPrompt !=null)
            {
                if (targetPosition == closedPosition)
                    openDoorPrompt.SetActive(true);
                else if (targetPosition == openPosition)
                    closeDoorPrompt.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exits the trigger zone
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;

            // Hide the interaction prompt
            if (openDoorPrompt != null || closeDoorPrompt !=null)
            {
                openDoorPrompt.SetActive(false);
                closeDoorPrompt.SetActive(false);
            }
        }
    }
}
