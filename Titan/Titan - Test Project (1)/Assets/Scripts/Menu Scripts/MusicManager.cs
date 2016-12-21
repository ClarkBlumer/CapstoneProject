using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{

    public AudioClip menuTheme;
    public AudioClip mainTheme;

    void Start()
    {
        AudioManager.instance.PlayMusic(menuTheme, 2); //gest the static instance of the AudioManager and passes in the menu theme and plays
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) //testing cross fade
        //{
        //    AudioManager.instance.PlayMusic(mainTheme, 3);

        //}
    }

}
