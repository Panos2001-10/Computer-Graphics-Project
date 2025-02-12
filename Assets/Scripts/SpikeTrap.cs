using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    // Public variables for the spike trap's settings
    public Transform spikes; // Reference to the spikes object
    public Vector3 loweredPosition; // Local position when spikes are lowered
    public Vector3 raisedPosition; // Local position when spikes are raised
    public float interval = 0.5f; // Time interval between switching states (raising/lowering)
    public float moveSpeed = 5f; // Speed at which the spikes move
    public int damage = 1; // Damage dealt to the player when triggered

    private bool isRising = false; // To determine if spikes are currently rising
    private float timer = 0f; // Timer to track the time interval between state changes
    private Vector3 targetPosition; // The target position the spikes are moving towards

    private void Start()
    {
        // Initialize the spikes at the lowered position when the trap starts
        if (spikes != null)
        {
            spikes.localPosition = loweredPosition; // Set spikes to their lowered position
            targetPosition = loweredPosition; // Set the target position to lowered
        }
    }

    private void Update()
    {
        // Exit if the spikes object is not assigned
        if (spikes == null)
        {
            return;
        }

        // Increment the timer by the time passed since the last frame
        timer += Time.deltaTime;

        // Check if it's time to toggle the spike position (raised/lowered)
        if (timer >= interval)
        {
            // Toggle the direction (isRising)
            isRising = !isRising;
            // Set the new target position based on whether the spikes are rising or lowering
            targetPosition = isRising ? raisedPosition : loweredPosition;
            // Reset the timer after an interval has passed
            timer = 0f;
        }

        // Smoothly move the spikes towards the target position using Lerp (linear interpolation)
        spikes.localPosition = Vector3.Lerp(spikes.localPosition, targetPosition, Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // When another collider enters the trigger, check if it's the player
        if (other.CompareTag("Player"))
        {
            // Attempt to get the PlayerManager component from the player object
            PlayerManager playerHealth = other.GetComponent<PlayerManager>();
            if (playerHealth != null)
            {
                // If PlayerHealth is found, apply damage to the player
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
