using UnityEngine;
using System.Collections;

public class IncreaseMaxAmmo : MonoBehaviour {
    public string target = "Player";
    public int ammoType = 1;
    public float amountIncrease = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == target)
        {
            AmmoBelt belt = other.gameObject.GetComponentInChildren<AmmoBelt>();
            if (belt)
            {
                Debug.Log("Ammo belt located");
                belt.addMaxAmmo(amountIncrease, ammoType);
                Destroy(gameObject);
            }
        }
    }
}
