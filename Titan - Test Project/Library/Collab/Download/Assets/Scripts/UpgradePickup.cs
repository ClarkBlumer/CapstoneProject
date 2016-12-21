using UnityEngine;
using System.Collections;

public class UpgradePickup : MonoBehaviour {

    //private Rigidbody2D character;
	// Use this for initialization
	void Start ()
    {
        //character = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("DoubleJump"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
