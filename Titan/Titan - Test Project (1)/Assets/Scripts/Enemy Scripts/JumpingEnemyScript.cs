using UnityEngine;
using System.Collections;

public class JumpingEnemyScript : MonoBehaviour {

    private bool? goRight = null;//right is true, left is false, and don't move is null.
    private bool jump = false;

    public float verticalSightThreshold = 10;
    public float horizontalSightThreshold = 16;

    public float findDirectionInterval = 0.9f;
    public float findDirectionIntervalVariation = 0.5f;
    private float actualDirectionFindInterval = 0f;

    public float moveSpeed = 2.5f;

    public float jumpInterval = 1.7f;
    public float jumpIntervalVariation = 0.9f;
    private float actualJumpInterval = 1.0f;

    public float jumpForce = 1600f;//default values tuned for a 100 mass object. Don't say I didn't warn you.
    public float jumpForceVariation = 400f;
    
    private Rigidbody2D myBody;

    void Awake () {
        myBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start(){
    }

    // Update is called once per frame
    void Update()
    {
        //find direction player is in after actualDirectionFindInterval seconds 
        actualDirectionFindInterval -= Time.deltaTime;
        if(actualDirectionFindInterval <= 0)
        {
            FindPlayerDirection();
            actualDirectionFindInterval = findDirectionInterval + Random.Range(-findDirectionIntervalVariation, findDirectionIntervalVariation);
        }
        //jump after actualJumpInterval seconds
        actualJumpInterval -= Time.deltaTime;
        if (actualJumpInterval <= 0)
        {
            if (goRight != null) jump = true;//prepare a jump if we are going in a direction.
            actualJumpInterval = jumpInterval + Random.Range(-jumpIntervalVariation, jumpIntervalVariation);
        }
    }

    void FixedUpdate()
    {
        //Move in desired direction; don't move if the player can't be found (goRight == null).
        Move();
    }

    void Move()
    {
        //handle jumping
        if (jump)
        {
            myBody.AddForce(new Vector2(0, jumpForce + Random.Range(-jumpForceVariation, jumpForceVariation)), ForceMode2D.Impulse);
            //reset jump
            jump = false;
        }
        //handle horizontal movement
        if (goRight == null) myBody.velocity = new Vector2(0, myBody.velocity.y); //don't move
        else if (goRight == true) myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);// go right
        else myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);// go left
    }

    void FindPlayerDirection()
    {
        //get player object
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //get/check distance against threshold
        if (player)
        {
            //Debug.Log("Found Player");
            Vector2 distance = player.transform.position - gameObject.transform.position;
            Vector3 temp = transform.localScale;
            //Debug.Log("distance x: " + distance.x);
            if (Mathf.Abs(distance.x) > horizontalSightThreshold
                || Mathf.Abs(distance.y) > verticalSightThreshold)
                goRight = null; //player is too far away
            else if (distance.x > 0)
            {
                goRight = true; //player is to the right
                temp.x = 1f;
            }
            else
            {
                goRight = false; //player is to the left (or in the exact same spot)
                temp.x = -1f;
            }

            //disable/enable left mode
            if(goRight != null)
            {
                AimAtPlayer[] aims = gameObject.GetComponentsInChildren<AimAtPlayer>();
                foreach (AimAtPlayer aim in aims)
                {
                    aim.leftMode = !(bool)goRight;
                }
            }
            
            //set flip rotation
            transform.localScale = temp;
            // jump when the player is right above or below, why not?
            if (distance.x == 0) jump = true;
        }
        
    }
}
