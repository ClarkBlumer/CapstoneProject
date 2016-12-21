using UnityEngine;
using System.Collections;

public class VelocityWithoutRigidbody : MonoBehaviour {

    public float bulletSpeed = 40;
    public Vector3 direction = Vector3.right;
    public float timeRemaining = -1; //negative to never stop moving

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * Time.deltaTime * bulletSpeed); //way to move things with or without a rigidbody.
        if (timeRemaining >= 0) {
            timeRemaining -= Time.deltaTime;
            if(timeRemaining <= 0)
                Destroy(this);
        }
    }
}
