using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audiomixer;
    public GameObject settingsmenu;

    public Dropdown resolutionDropdown;
    public Slider volumeSlider;
    public Dropdown qualityDropDown;
    public Toggle fullscreenCheckbox;

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);

        int resolutionIndex = PlayerPrefs.GetInt("resolutionIndex");
        float volume = PlayerPrefs.GetFloat("volume");
        int qualityIndex = PlayerPrefs.GetInt("qualityIndex");
        bool fullscreen = PlayerPrefs.GetInt("PropName") == 1 ? true : false;

        SetResolution(resolutionIndex);
        resolutionDropdown.value = resolutionIndex;
        resolutionDropdown.RefreshShownValue();

        SetVolume(volume);
        volumeSlider.value = volume;

        SetQuality(qualityIndex);
        qualityDropDown.value = qualityIndex;
        qualityDropDown.RefreshShownValue();

        SetFullscreen(fullscreen);
        fullscreenCheckbox.isOn = fullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("resolutionIndex", resolutionIndex);
        PlayerPrefs.Save();
    }
    
    public void SetVolume(float volume)
    {
        audiomixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("qualityIndex", qualityIndex);
        PlayerPrefs.Save();
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("PropName", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ToggleSettings()
    {
        settingsmenu.SetActive(!settingsmenu.activeSelf);
    }
}
