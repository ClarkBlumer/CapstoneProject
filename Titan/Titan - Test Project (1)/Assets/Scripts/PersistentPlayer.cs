using UnityEngine;
using System.Collections;

public class PersistentPlayer : MonoBehaviour {

    public static PersistentPlayer player;

    void Awake()
    {
        // if there isn't a GameControl object already, will make the one and only one
        if (player == null)
        {
            DontDestroyOnLoad(gameObject); //GameControl object will persist through scenes if we go that route
            player = this;
        }
        else if (player != this)
        {
            //if there is another GameControl
            Destroy(gameObject);
        }
    }
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
