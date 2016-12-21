using UnityEngine;
using System.Collections;

public class SpiderWalker : MonoBehaviour {

    [SerializeField]
    private Transform startPos, endPos;

    private bool collsion;

    public float speed = 1f;

    private Rigidbody2D myBody;

    void Awake()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>();
    }

	// Update is called once per frame
	void FixedUpdate () {
        Move();
        ChangeDirection();
	}

    void ChangeDirection()
    {
        collsion = Physics2D.Linecast(startPos.position, endPos.position, 1 << LayerMask.NameToLayer("Ground"));
        Debug.DrawLine(startPos.position, endPos.position, Color.green);

        if (!collsion) // if the spider is no longer colliding with the ground
        {
            Vector3 temp = transform.localScale; // scale is the
            if(temp.x == 1f) // Check if spider is aiming right
            {
                temp.x = -1f; //face left
            }
            else // is facing left
            {
                temp.x = 1f; // face right
            }

            transform.localScale = temp;
        }
    }

    void Move ()
    {
        myBody.velocity = new Vector2(transform.localScale.x, 0) * speed;
    }

    void OnCollsionEnter(Collision2D target)
    {
        if(target.gameObject.tag == "PlayerLayer")
        {
           
        }
    }
} // Spider Walker
