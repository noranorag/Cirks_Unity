using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionController : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private const string RESOLUTION_PREF_KEY = "SelectedResolution";

    private void Start()
    {
        InitializeResolutions();
        LoadSavedResolution();
    }

    private void InitializeResolutions()
    {
        resolutions = new Resolution[]
        {
            new Resolution { width = 1920, height = 1080 },
            new Resolution { width = 1280, height = 980 },
            new Resolution { width = 1280, height = 720 }
        };

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(new System.Collections.Generic.List<string>
        {
            "1920 x 1080",
            "1280 x 980",
            "1280 x 720"
        });

        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
    }

    private void LoadSavedResolution()
    {
        int savedResIndex = PlayerPrefs.GetInt(RESOLUTION_PREF_KEY, 0);
        resolutionDropdown.value = savedResIndex;
        SetResolution(savedResIndex);
    }

    public void OnResolutionChanged(int index)
    {
        SetResolution(index);
        PlayerPrefs.SetInt(RESOLUTION_PREF_KEY, index);
        PlayerPrefs.Save();
    }

    private void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}