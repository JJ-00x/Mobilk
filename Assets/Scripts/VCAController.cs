using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class VCAController : MonoBehaviour
{
    public Slider musicSlider;     // Reference to the Music Volume slider
    public Slider sfxSlider;       // Reference to the SFX Volume slider

    private FMOD.Studio.VCA musicVCA;   // FMOD VCA for Music
    private FMOD.Studio.VCA sfxVCA;     // FMOD VCA for SFX

    void Start()
    {
        // Get VCA references using their FMOD Studio paths
        musicVCA = RuntimeManager.GetVCA("vca:/Music");
        sfxVCA = RuntimeManager.GetVCA("vca:/SFX");

        // Set default volume values and listeners for slider changes
        musicSlider.value = 1f;
        sfxSlider.value = 1f;

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float volume)
    {
        musicVCA.setVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVCA.setVolume(volume);
    }
}
