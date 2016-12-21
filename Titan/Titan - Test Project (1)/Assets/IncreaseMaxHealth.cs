using UnityEngine;
using System.Collections;

public class IncreaseMaxHealth : MonoBehaviour {
    public float amountIncrease = 15.0f;
    public string target = "Player";

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == target)
        {
            //Debug.Log("Player entered!");
            //throw new System.Exception("Player entered!");

            Health h = other.gameObject.GetComponent<Health>();
            if (h)
            {
                h.maxHealth += amountIncrease;
                h.currentHealth = h.maxHealth;
            }
            Destroy(gameObject);
        }
    }
}
