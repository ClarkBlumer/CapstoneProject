using UnityEngine;
using System.Collections;

public class AddChildWhileTouching : MonoBehaviour {

    
    private GameObject player;
    public string childTag = "Player";
    public GameObject parent;

    // Use this for initialization
    void Awake()
    {
        parent = gameObject;
    }
	// Update is called once per frame
	
    void OnCollisionStay2D(Collision2D target)
    {
        if (target.gameObject.tag == "Player" && parent)
            target.gameObject.transform.SetParent(parent.transform);
    }

    void OnCollisionExit2D(Collision2D target)
    {
        if (target.gameObject.tag == "Player")
            target.gameObject.transform.SetParent(null);
    }
}
