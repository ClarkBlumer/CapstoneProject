using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public enum AudioChannel { Master, SFX, Music};

    public float masterVolumePercent { get; private set; }
    public float musicVolumePercent { get; private set; }
    public float sfxVolumePercent { get; private set; }

    //Two audio sources to crossfade if we want/need
    AudioSource sfx2DSource;
    AudioSource[] musicSources;
    int activeMusicSourceIndex; //keeps track of which music source is playing: 0 or 1

    public static AudioManager instance; //make it a singleton

    Transform audioListener; //get reference of audio listener


    SoundLibrary library;


    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);//to prevent duplicate AudioManagers
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // AudioManager persists through scene changes
            library = GetComponent<SoundLibrary>();
            musicSources = new AudioSource[2];
            for (int i = 0; i < 2; i++)
            {
                GameObject newMusicSource = new GameObject("Music Source " + (i + 1));
                musicSources[i] = newMusicSource.AddComponent<AudioSource>();
                newMusicSource.transform.parent = transform; //parent to audiomanager gameobject
            }

            audioListener = FindObjectOfType<AudioListener>().transform;

            //All sounds were currently 3D spaced, this one we can use for maybe more appropriate 2D sounds.  Level complete, maybe boss roars that 
            //encompass the whole area?
            GameObject newSFX2DSource = new GameObject("2D SFX Source");
            sfx2DSource = newSFX2DSource.AddComponent<AudioSource>();
            newSFX2DSource.transform.parent = transform;

            //Gets player prefs for sound levels
            masterVolumePercent = PlayerPrefs.GetFloat("master vol", 1);
            sfxVolumePercent = PlayerPrefs.GetFloat("sfx vol", 1);
            musicVolumePercent = PlayerPrefs.GetFloat("music vol", 1);
        }
    }



    // Use this for initialization
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {

    }



    public void PlayMusic(AudioClip clip, float fadeDuration = 1)
    {
        activeMusicSourceIndex = 1 - activeMusicSourceIndex; // 0 1, 0 1, 0 1, so on
        musicSources[activeMusicSourceIndex].clip = clip;
        musicSources[activeMusicSourceIndex].Play();
        musicSources[activeMusicSourceIndex].loop = true;

        StartCoroutine(AnimateMusicCrossfade(fadeDuration));
    }




    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        //PlayClipAtPoint good for sfx because you cannot change volume while it plays
        //Best used with short clips
        if(clip != null)
            AudioSource.PlayClipAtPoint(clip, pos, sfxVolumePercent * masterVolumePercent); //playclipatpoint for 3D points
    }



    /// <summary>
    /// uses soundName as a string to find the clip in our library
    /// </summary>
    /// <param name="soundName"></param>
    /// <param name="pos">Vector3 to get 3D position where sound should play</param>
    public void PlaySound(string soundName, Vector3 pos)
    {
        PlaySound(library.GetClipFromName(soundName), pos);
    }



    public void PlaySound2D(string soundName)
    {
        //gets clip from library, and applies volume
        sfx2DSource.PlayOneShot(library.GetClipFromName(soundName), sfxVolumePercent * masterVolumePercent);
    }




    IEnumerator AnimateMusicCrossfade(float duration)
    {
        float percent = 0; //how far into the crossfade we are
        while(percent < 1) //while still crossfading
        {
            percent += Time.deltaTime * 1 / duration;
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolumePercent * masterVolumePercent, percent);//one we're fading into
            musicSources[1 - activeMusicSourceIndex].volume = Mathf.Lerp(musicVolumePercent * masterVolumePercent, 0, percent);//one we're fading out of
            yield return null;
        }
    }




    public void SetVolume(float volumePercent, AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.Master:
                masterVolumePercent = volumePercent;
                break;
            case AudioChannel.SFX:
                sfxVolumePercent = volumePercent;
                break;
            case AudioChannel.Music:
                musicVolumePercent = volumePercent;
                break;
        }

        musicSources[0].volume = musicVolumePercent * masterVolumePercent;
        musicSources[1].volume = musicVolumePercent * masterVolumePercent;

        //Saves player prefs for sound levels
        PlayerPrefs.SetFloat("master vol", masterVolumePercent);
        PlayerPrefs.SetFloat("music vol", musicVolumePercent);
        PlayerPrefs.SetFloat("sfx vol", sfxVolumePercent);
        PlayerPrefs.Save();
    }

}
