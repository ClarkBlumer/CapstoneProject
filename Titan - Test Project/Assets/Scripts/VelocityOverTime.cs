using UnityEngine;
using System.Collections;
using System;

public class VelocityOverTime : MonoBehaviour {
    
    public Vector2 newVelocity = Vector2.right;
    public float timeRemaining = -1; //negative to never stop moving
    public Rigidbody2D myBody;
    public bool overrideWithZero = true;
    public Vector2 forceAfter = new Vector2();

    // Use this for initialization
    void Start()
    {
        if (myBody == null) GetMyBody();
    }

    // Update is called once per frame
    void Update()
    {
        //count down to zero if not already below 0
        if (timeRemaining >= 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                if (myBody == null)
                    GetMyBody();//get body if not found
                if (myBody != null)
                    myBody.AddForce(forceAfter);
                Destroy(this);
            }
        }
    }

    void FixedUpdate()
    {
        if (myBody == null)
            GetMyBody();//get body if not found
        if (myBody != null)
        {
            if (!overrideWithZero)
            {
                if (newVelocity.x == 0)
                    newVelocity.x = myBody.velocity.x;
                if (newVelocity.y == 0)
                    newVelocity.y = myBody.velocity.y;
            }
            myBody.velocity = (newVelocity);//set velocity if found
        }
    }

    private void GetMyBody()
    {
        Rigidbody2D rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        if (rigidBody2D != null) myBody = rigidBody2D;
    }
}
