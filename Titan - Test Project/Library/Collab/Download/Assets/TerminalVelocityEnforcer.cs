using UnityEngine;
using System.Collections;

public class TerminalVelocityEnforcer : MonoBehaviour {

    public float terminalVelocity = 10.0f;
    private Rigidbody2D thing; 
	// Use this for initialization
	void Start () {
        thing = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
