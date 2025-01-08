using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public Transform spikes; // Reference to the spikes object
    public Vector3 loweredPosition; // Local position when spikes are lowered
    public Vector3 raisedPosition; // Local position when spikes are raised
    public float interval = 0.5f; // Time interval between switching states
    public float moveSpeed = 5f; // Speed at which the spikes move

    private bool isRising = false; // To determine if spikes are currently rising
    private float timer = 0f; // Timer to track intervals
    private Vector3 targetPosition; // Target position for spikes

    private void Start()
    {
        // Initialize the spikes at the lowered local position
        if (spikes != null)
        {
            spikes.localPosition = loweredPosition;
            targetPosition = loweredPosition;
        }
    }

    private void Update()
    {
        if (spikes == null)
        {
            return;
        }

        // Update the timer
        timer += Time.deltaTime;

        // Check if it's time to toggle the spike position
        if (timer >= interval)
        {
            isRising = !isRising; // Toggle the direction
            targetPosition = isRising ? raisedPosition : loweredPosition; // Set the new target position
            timer = 0f; // Reset the timer
        }

        // Smoothly move the spikes toward the target position
        spikes.localPosition = Vector3.Lerp(spikes.localPosition, targetPosition, Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit by spikes!");
            // Add damage logic here
        }
    }
}
