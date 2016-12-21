using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class Mutator : MonoBehaviour {

    public bool GiveSquish = false;
    public bool GiveDoubleJump = false;
    public bool GiveWallJump = false;
     
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
            //Debug.Log("Collided with player");
            PlatformerCharacter2D playerCharacter2D = collider.gameObject.GetComponent<PlatformerCharacter2D>();

            if (playerCharacter2D)
            {
                if (playerCharacter2D != null && playerCharacter2D.m_CanDoubleJump && GiveDoubleJump)
                {
                    Debug.Log("Giving player double jump");
                    playerCharacter2D.m_HasDoubleJumpUpgrade = true;
                }

                if (GiveSquish)
                {
                    Debug.Log("Giving player squish");
                    playerCharacter2D.m_HasSquishUpgrade = true;
                }

                if (GiveWallJump)
                {
                    playerCharacter2D.whatIsWall = 1 << 14;
                }

                //This way an object can impart more than one ability at a time
                Destroy(gameObject);
            }
        }
    }
}
