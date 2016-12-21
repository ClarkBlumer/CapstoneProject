using UnityEngine;
using System.Collections;

public class Interacter : MonoBehaviour {

    public float interactRadius = 2.0f;
    
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        //when 'E' is pressed interact with things in range
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, interactRadius);
	}
}
