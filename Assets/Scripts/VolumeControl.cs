using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider; 
    private AudioSource audioSource; 

    private const string VolumeKey = "Volume"; 

    void Start()
    {
  
        audioSource = GameObject.Find("Background music").GetComponent<AudioSource>();

        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 0.5f);
        audioSource.volume = savedVolume; 
        volumeSlider.value = savedVolume; 

        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void ChangeVolume(float volume)
    {
        audioSource.volume = volume; 
        PlayerPrefs.SetFloat(VolumeKey, volume); 
        PlayerPrefs.Save(); 
    }
}