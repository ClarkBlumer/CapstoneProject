using UnityEngine;
using System.Collections;
using System;

public class DestroyOnTimerOrCollisions : MonoBehaviour {

    public float timeTilDestroyed = -1;
    public int collisionsTilDestroyed = -1;
    public string targetTag;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (timeTilDestroyed >= 0)
        {
            timeTilDestroyed -= Time.deltaTime;
            if (timeTilDestroyed < 0)
                Destroy(gameObject);
        }
        if(timeTilDestroyed == 0)
            Destroy(gameObject);
        if (collisionsTilDestroyed == 0)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log(gameObject.name + "Entering collision with " + collision.gameObject.name);
        if (targetTag == null || targetTag == "" || collision.gameObject.tag == targetTag)
            decrementCollisions();
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log(gameObject.name + "Entering collision with " + collider.gameObject.name);
        if (targetTag == null || targetTag == "" || collider.gameObject.tag == targetTag)
            decrementCollisions();
    }
    
    private void decrementCollisions()
    {
        if (collisionsTilDestroyed >= 0)
        {
            collisionsTilDestroyed--;
            if (collisionsTilDestroyed <= 0)
                Destroy(gameObject);
        }
    }
}
