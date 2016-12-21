    using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public Transform endPos, startPos;

    private bool collsion, obstacle = false, player = false, isFalling = false;
    private Rigidbody2D myBody;
    public Vector3 wallCheck;
    
    [SerializeField]
    private LayerMask m_WhatIsGround;
    [SerializeField]
    private LayerMask m_WhatIsObsticle;
    private bool wallCollision = false;

    public float speed = 1f;

    void Awake()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>();

        Transform[] t = gameObject.GetComponentsInChildren<Transform>();
        if (startPos == null && t.Length > 0)
            startPos = t[0];

        if(endPos == null && t.Length > 1)
            endPos = t[1];
    }

	void FixedUpdate () {
        Move();
        ChangeDirection();
	}

    void ChangeDirection()
    {
        collsion = Physics2D.Linecast(startPos.position, endPos.position, m_WhatIsGround);
        isFalling = !Physics2D.Linecast(gameObject.transform.position, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1.0f), m_WhatIsGround);
        //wallCollision = Physics2D.Linecast(gameObject.transform.position, wallCheck, m_WhatIsGround); // check to see if collide with anything

        Debug.DrawLine(startPos.position, endPos.position, Color.green);
        Debug.DrawLine(gameObject.transform.position, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1.0f), Color.green);
        //Debug.DrawLine(gameObject.transform.position, wallCheck, Color.green);
        //Debug.DrawRay(startPos.position, new Vector2(0, -100), Color.green);
        if (!collsion || player) // if the spider is no longer colliding with the ground or the spider hits an obsticale
        {
            if (!isFalling)
            {
                Vector3 temp = transform.localScale; // scale is the
                if (temp.x == 1f) // Check if spider is aiming right
                {
                    temp.x = -1f; //face left
                }
                else // is facing left
                {
                    temp.x = 1f; // face right
                }

                transform.localScale = temp;
                obstacle = false; // We have changed our direction. No more obsticale
                player = false;
            }
            
        }
    }

    void Move ()
    {
        if (!isFalling)
        {
            myBody.velocity = new Vector2(transform.localScale.x, 0) * speed;
        }
    }

   void OnCollisionEnter2D(Collision2D target)
    {
        //TEST
        //Debug.Log("A Collsion has occured between slime and " + target.gameObject.tag);

        //Debug.Log("Value BEFORE mask comparision: " + (m_WhatIsObsticle.value));
        //Debug.Log("Obsticals are:" + System.Convert.ToString(m_WhatIsObsticle.value, 2) + " (int " + m_WhatIsObsticle.value + ")");//target.gameObject.layer);
        //Debug.Log("target    are:" + System.Convert.ToString(1 << target.gameObject.layer, 2));//target.gameObject.layer);
        //Debug.Log("Value AFTER mask comparision: " + (m_WhatIsObsticle.value ^ (1 << target.gameObject.layer)));

        if (target.gameObject.tag == "Obsticle" || ((m_WhatIsObsticle.value ^ (1<<target.gameObject.layer)) < m_WhatIsObsticle.value))
        {
            
            player = true;
        }
    }

} // Slime-er
