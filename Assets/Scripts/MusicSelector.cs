using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class MusicSelector : MonoBehaviour
{
    public AudioSource audioSource; 
    public Dropdown musicDropdown; 
    public AudioClip[] musicClips; 

    private const string MusicKey = "SelectedMusicIndex"; 

    void OnEnable()
    {
        PopulateDropdown(); 
    }

    private void PopulateDropdown()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned in the Inspector.");
            return;
        }

        if (musicDropdown == null)
        {
            Debug.LogError("Dropdown is not assigned in the Inspector.");
            return;
        }

        if (musicClips == null || musicClips.Length == 0)
        {
            Debug.LogError("Music clips array is empty or not assigned in the Inspector.");
            return;
        }

        musicDropdown.ClearOptions(); 
        List<string> options = new List<string>(); 

        foreach (AudioClip clip in musicClips)
        {
            options.Add(clip.name); 
        }

        musicDropdown.AddOptions(options); 

        int savedIndex = PlayerPrefs.GetInt(MusicKey, 0);
        musicDropdown.value = savedIndex; 

        if (savedIndex < musicClips.Length)
        {
            audioSource.clip = musicClips[savedIndex]; 
            audioSource.Play(); 
        }
    }

    void Start()
    {
        musicDropdown.onValueChanged.AddListener(ChangeMusic);
    }

    public void ChangeMusic(int index)
    {
        if (index < musicClips.Length)
        {
            audioSource.clip = musicClips[index]; 
            audioSource.Play(); 
            PlayerPrefs.SetInt(MusicKey, index); 
            PlayerPrefs.Save(); 
        }
        else
        {
            Debug.LogWarning("Index out of range when changing music: " + index);
        }
    }
}