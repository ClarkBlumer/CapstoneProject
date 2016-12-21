using UnityEngine;
using System.Collections;

public class DoubleJumper : MonoBehaviour {

    public int doubleJumpsMax = 1;
    int jumpsLeft;


	// Use this for initialization
	void Start () {
        //init
        jumpsLeft = doubleJumpsMax;

    }
	
	// Update is called once per frame
	void Update () {
        //if on ground, and don't have appropriate # of jumps, restore jumps
        if (jumpsLeft != doubleJumpsMax) { //AND ON GROUND
            jumpsLeft = doubleJumpsMax;
        }
        //if space is pressed while in the air and while jumpsLeft > 0, do a jump
        if (jumpsLeft > 0) {//AND IN AIR AND SPACE IS PRESSED
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up, ForceMode2D.Impulse);
            jumpsLeft--;
        }
	}
}
