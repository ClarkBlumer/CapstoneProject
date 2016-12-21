using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public Slider MusicVolumeSlider;
    public Slider MasterVolumeSlider;
    public Slider SFXVolumeSlider;
    //public AudioSource musicSource;
    public GameSettings gameSettings;
    

    void OnEnable()
    {
        gameSettings = new GameSettings();
        MasterVolumeSlider.onValueChanged.AddListener(delegate { MasterVolumeChange(); });
        MusicVolumeSlider.onValueChanged.AddListener(delegate { MusicVolumeChange(); });
        SFXVolumeSlider.onValueChanged.AddListener(delegate { SFXVolumeChange(); });
    }

    public void MasterVolumeChange()
    {
        AudioListener.volume = gameSettings.MasterVolume = MasterVolumeSlider.value;
    }

    public void MusicVolumeChange()
    {
        var musicVolumes = Object.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach(var audioSource in musicVolumes)
            gameSettings.MusicVolume = MusicVolumeSlider.value;
    }

    public void SFXVolumeChange()
    {
        var SFXVolumes = Object.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (var audioSource in SFXVolumes)
            gameSettings.SFXVolume = SFXVolumeSlider.value;
    }

    public void SaveSettings()
    {

    }

    public void LoadSettings()
    {

    }

	// Use this for initialization
	void Start () {
        AudioListener.volume = gameSettings.MasterVolume = MasterVolumeSlider.value;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
