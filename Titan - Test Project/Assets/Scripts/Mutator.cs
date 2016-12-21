using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class Mutator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Interact (GameObject go)
    {
        //for the moment, only gives Double Jump to player
        go.AddComponent<DoubleJumper>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PlatformerCharacter2D playerCharacter2D = collider.gameObject.GetComponent<PlatformerCharacter2D>();
            if (playerCharacter2D != null && playerCharacter2D.m_CanDoubleJump)
            {
                Debug.Log("Giving player double jump");
                playerCharacter2D.m_HasDoubleJumpUpgrade = true;
                Destroy(gameObject);
            }
        }
    }
}
