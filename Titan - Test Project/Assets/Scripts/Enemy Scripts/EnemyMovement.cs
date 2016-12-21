    using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public Transform endPos, startPos;

    private bool collsion, obstacle = false, player = false, isFalling = false;
    private Rigidbody2D myBody;
    [SerializeField]
    private LayerMask m_WhatIsGround;
    [SerializeField]
    private LayerMask m_WhatIsObsticle;

    public float speed = 1f;

    void Awake()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>();
        if (startPos == null)
            startPos = gameObject.GetComponent<RectTransform>();
    }

	void FixedUpdate () {
        Move();
        ChangeDirection();
	}

    void ChangeDirection()
    {
        collsion = Physics2D.Linecast(startPos.position, endPos.position, m_WhatIsGround);
        isFalling = !Physics2D.Linecast(gameObject.transform.position, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1.0f), m_WhatIsGround);
        Debug.DrawLine(startPos.position, endPos.position, Color.green);
        Debug.DrawLine(gameObject.transform.position, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1.0f), Color.green);
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
       
        if(target.gameObject.tag == "Obsticle" || !(m_WhatIsObsticle.value & 1<<target.gameObject.layer).Equals(0))
        {
            player = true;
        }
    }

} // Slime-er
