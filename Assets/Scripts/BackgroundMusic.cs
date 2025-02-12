using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep playing music between scenes
            audioSource = GetComponent<AudioSource>(); // Get AudioSource component
        }
        else
        {
            Destroy(gameObject); // Prevent multiple instances
        }
    }

    public static void StartMusic()
    {
        if (instance != null && instance.audioSource != null && !instance.audioSource.isPlaying)
        {
            instance.audioSource.Play();
        }
    }

    public static void StopMusic()
    {
        if (instance != null && instance.audioSource != null)
        {
            instance.audioSource.Stop();
        }
    }
}
