using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtonManager : MonoBehaviour {

    public GameObject mainMenuHolder;
    public GameObject optionsMenuHolder;
    public Slider[] volumeSliders;

    
    void Start()
    {
        Time.timeScale = 1;
        Cursor.visible = true;
        //Gets the playerprefs since those are handled in the AudioManager
        volumeSliders[0].value = AudioManager.instance.masterVolumePercent;
        volumeSliders[1].value = AudioManager.instance.musicVolumePercent;
        volumeSliders[2].value = AudioManager.instance.sfxVolumePercent;


    }

    public void NewGameButton()
    {
        SceneManager.LoadScene("Base");
    }


    public void LoadGameButton()
    {
        //Interesting code to load a save state goes here!
        GameControl.control.Load();
    }



    public void OptionsButton()
    {
        //Transition to new scene just like in NewGameButton()
        //SceneManager.LoadScene(sceneIndex);
        mainMenuHolder.SetActive(false);
        optionsMenuHolder.SetActive(true);
    }

    public void BackButton()
    {
        mainMenuHolder.SetActive(true);
        optionsMenuHolder.SetActive(false);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif        
    }



    public void SetMasterVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master);
    }



    public void SetMusicVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Music);
    }



    public void SetSFXVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.SFX);
    }


}
