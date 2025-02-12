using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Singleton instance to ensure only one instance of BackgroundMusic exists
    private static BackgroundMusic instance;
    private AudioSource audioSource; // Reference to the AudioSource component

    private void Awake()
    {
        // Ensure only one instance of BackgroundMusic exists across scenes
        if (instance == null)
        {
            instance = this; // Set this instance as the singleton
            DontDestroyOnLoad(gameObject); // Keep this object between scene transitions
            audioSource = GetComponent<AudioSource>(); // Get the AudioSource component attached to this object
        }
        else
        {
            // If another instance exists, destroy the new one to prevent duplicates
            Destroy(gameObject);
        }
    }

    // Static method to start the background music
    public static void StartMusic()
    {
        // Check if the instance and AudioSource are valid and if music is not already playing
        if (instance != null && instance.audioSource != null && !instance.audioSource.isPlaying)
        {
            instance.audioSource.Play(); // Play the background music
        }
    }

    // Static method to stop the background music
    public static void StopMusic()
    {
        // Check if the instance and AudioSource are valid
        if (instance != null && instance.audioSource != null)
        {
            instance.audioSource.Stop(); // Stop the background music
        }
    }
}
