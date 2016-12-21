using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundLibrary : MonoBehaviour {

    public SoundGroup[] soundGroups;

    //Dictionary of all the sound fx and their IDs
    Dictionary<string, AudioClip[]> groupDictionary = new Dictionary<string, AudioClip[]>();


    void Awake()
    {
        //loop through sound groups and add to dictionary
        foreach(SoundGroup soundGroup in soundGroups)
        {
            groupDictionary.Add(soundGroup.groupID, soundGroup.group);
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public AudioClip GetClipFromName(string name)
    {
        //Return a random sound if multiple clips share a similar name
        if (groupDictionary.ContainsKey(name))
        {
            AudioClip[] sounds = groupDictionary[name];
            return sounds[Random.Range(0, sounds.Length)];
        }
        return null; //if no value in dictionary with given key name
    }


    [System.Serializable]
    public class SoundGroup
    {
        public string groupID;
        public AudioClip[] group;
    }
}
