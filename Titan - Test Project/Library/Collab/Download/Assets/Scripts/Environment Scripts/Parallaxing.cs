using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgrounds;         //Array that stores all the items that need parallaxing (backgrounds/foregrounds)
    private float[] parallaxScales;         //The proportion of the camera's movement to move the backgrounds
    public float parallaxAmount = 1f;       //How smooth the parallax will be.  Be sure to set this greater than zero

    private Transform cam;                  //Reference to main camera transform
    private Vector3 previousCamPosition;    //Stores the position of the camera in the previous frame
    
    
    //Called before Start(), call all the logic before Start(), but before all the game objects are setup
    void Awake ()
    {
        //Set up camera reference
        cam = Camera.main.transform;
    }

	// Use this for initialization
	void Start ()
    {
        //Previous frame had the current frame's camera position
        previousCamPosition = cam.position;

        //Assigning coresponding parallaxScales
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1; 
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        //For each background
	    for(int i = 0; i < backgrounds.Length; i++)
        {
            //Parallax is the opposite of the camera movement because previous frame multiplied by the scale
            float parallax = (previousCamPosition.x - cam.position.x) * parallaxScales[i];

            //Set target x position which is the current postion + parallax
            float backgroundTargetPositionX = backgrounds[i].position.x + parallax;

            //Create target position which is the background's current position with its target x position
            Vector3 backgroundTargetPosition = new Vector3(backgroundTargetPositionX, backgrounds[i].position.y, backgrounds[i].position.z); //takes same y and z it had before

            //Fade between current position and target position using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPosition, parallaxAmount * Time.deltaTime);
        }

        //Set previous cam position to the camera's position at the end of the frame
        previousCamPosition = cam.position;
	}
}
