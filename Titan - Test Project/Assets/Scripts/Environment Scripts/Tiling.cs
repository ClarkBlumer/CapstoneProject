using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

    public int offsetX = 2; 

    //does the sprite have a buddy to either side
    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;

    //Make backgrounds reverse to appear smooth
    public bool reverseScale = false;

    private float spriteWidth = 0f;
    private Camera cam;
    private Transform myTransform;



    void Awake ()
    {
        cam = Camera.main;
        myTransform = transform;
    }

	// Use this for initialization
	void Start ()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x; //Get width of the element/sprite
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Check if it needs buddies, if not do nothing
        if (hasALeftBuddy == false || hasARightBuddy == false)
        {
            //Calculate the camera's extend half the width of what the camera can see (world coords instead of pixels)
            float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;

            //Calculate the x position where the camera can see the edge of the sprite
            float edgeVisablePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisablePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;

            //Check to see if the pos of the camera is bigger than or equal to the edge where the element is visable AND if it doesnt have a buddy
            if (cam.transform.position.x >= edgeVisablePositionRight - offsetX && hasARightBuddy == false) //check for right side
            {
                //Calls MakeNewBuddy()
                MakeNewBuddy(1); // 1 for right side -1 for left side
                hasARightBuddy = true;
            }
            else if (cam.transform.position.x <= edgeVisablePositionLeft + offsetX && hasALeftBuddy == false)//check for left side
            {
                MakeNewBuddy(-1);
                hasALeftBuddy = true;
            }
        }
	}

    // Method to create another instance of the sprite on whatever side required
    void MakeNewBuddy(int rightOrLeft)
    {
        //Calculate the new position for the new buddy.
        Vector3 newPosition = new Vector3(myTransform.position.x + myTransform.localScale.x * spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);

        //Instantiate a new buddy and store in myBuddy
        Transform newBuddy = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;

        //Flips sprite to get ride of ugly tiling effect
        if (reverseScale == true)
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z); // * -1 flips along the X axis
        }

        //newBuddy gets the same parent as the old one
        newBuddy.parent = myTransform.parent;


        if (rightOrLeft > 0)
        {
            newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;

        }
        else
        {
            newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
        }
    }
}
