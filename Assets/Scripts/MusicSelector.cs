using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MusicSelector : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource
    public Dropdown musicDropdown; // Reference to the Dropdown
    public AudioClip[] musicClips; // Array to hold different music clips

    private const string MusicKey = "SelectedMusicIndex"; // Key for PlayerPrefs
    private static MusicSelector instance; // Singleton instance

    void Awake()
    {
        // Check if an instance already exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instance
            return;
        }

        instance = this; // Set the singleton instance
        DontDestroyOnLoad(gameObject); // Keep this object alive across scenes
    }

    void Start()
    {
        // Populate the dropdown with the names of the audio clips
        musicDropdown.ClearOptions();
        List<string> options = new List<string>();

        foreach (AudioClip clip in musicClips)
        {
            options.Add(clip.name); // Add the name of each clip to the options
        }

        musicDropdown.AddOptions(options); // Add options to the dropdown

        // Load the saved music index or set to default (0)
        int savedIndex = PlayerPrefs.GetInt(MusicKey, 0);
        musicDropdown.value = savedIndex; // Set the dropdown to the saved index
        audioSource.clip = musicClips[savedIndex]; // Set the AudioSource clip to the saved clip
        audioSource.Play(); // Start playing the selected clip

        // Add a listener to the dropdown to call the ChangeMusic method when the value changes
        musicDropdown.onValueChanged.AddListener(ChangeMusic);
    }

    // Method to change the music
    public void ChangeMusic(int index)
    {
        audioSource.clip = musicClips[index]; // Change the audio clip based on the dropdown selection
        audioSource.Play(); // Play the new audio clip
        PlayerPrefs.SetInt(MusicKey, index); // Save the selected music index
        PlayerPrefs.Save(); // Ensure the PlayerPrefs are saved
    }
}