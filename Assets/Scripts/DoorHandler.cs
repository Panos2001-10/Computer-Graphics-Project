using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public Transform door; // The door object to slide
    public Vector3 openPosition; // The local position of the door when open
    public Vector3 closedPosition; // The local position of the door when closed
    public float slideSpeed = 2f; // Speed at which the door slides

    private bool isPlayerNearby = false;
    private bool isOpen = false;
    private Vector3 targetPosition;

    private void Start()
    {
        // Initialize the target position to the closed position
        targetPosition = closedPosition;
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            isOpen = !isOpen;
            targetPosition = isOpen ? openPosition : closedPosition;
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exits the trigger zone
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
