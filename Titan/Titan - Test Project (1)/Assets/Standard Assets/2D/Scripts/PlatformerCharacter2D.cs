using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] public float m_JumpForce = 400f;                  // Amount of force added when 
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
        [SerializeField] private LayerMask m_WhatIsCeiling;                  // A mask determining what is ground to the character
        

        public Transform wallCheck;         // A position marking where to check if player is touching a wall for wall jumping
        const float wallTouchRadius = .2f;  // radius of the overlap circle to determine if touching wall
        bool touchingWall;                  // Whether or the player is touching a wall
        public LayerMask whatIsWall;
        public float jumpPushForce = 10f;   //Force to push off walls


        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        public bool m_Grounded;             // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        public Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        public bool m_HasDoubleJumpUpgrade = false;// Check to see if player has acquired the double jump upgrade
        public bool m_CanDoubleJump = false;// Determine if player is able to double jump

        public bool m_HasSquishUpgrade = false;
        public bool m_SquishEnabled = false;
        private bool m_isSquished = false;
        private Vector3 m_originalScale;
        private Vector3 m_currentScale;

        Transform playerGraphics;           //Reference to player graphics so we can change direction

        //public Vector3 targetScale;
        //[Range(0,1)]public float scaleSpeed;
        //private Vector3 originalScale;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_originalScale = GetComponent<Transform>().localScale;
            //characterController = GetComponent<CharacterController>();

            //Added by Clark Blumer. Finds child of player object. Let's us modify the graphics instead of the actual player object
            playerGraphics = transform.FindChild("Graphics");
            if (playerGraphics == null)
                Debug.LogError("No Graphics object as a child of the player!");
        }



        private void FixedUpdate()
        {
            m_Grounded = false;
            
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            m_Anim.SetBool("Ground", m_Grounded);
            touchingWall = Physics2D.OverlapCircle(wallCheck.position, wallTouchRadius, whatIsWall);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }


        void Update()
        {
            if(touchingWall && Input.GetButtonDown("Jump"))
            {
                
                WallJump();
            }
        }

        void WallJump()
        {
            if (!m_FacingRight)
            {
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
                //m_Rigidbody2D.AddForce(new Vector2(jumpPushForce, 3) * (m_JumpForce / 2));
                m_Rigidbody2D.AddForce(Vector2.up * (m_JumpForce * .75f), ForceMode2D.Force);

            }
            if (m_FacingRight)
            {
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
                //m_Rigidbody2D.AddForce(new Vector2(-jumpPushForce, 3) * (m_JumpForce /  2));
                m_Rigidbody2D.AddForce(Vector2.up * (m_JumpForce * .75f), ForceMode2D.Force);
            }
        }
        public void Move(float move, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            var ceiling = Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsCeiling);
            // Let's the animator know that their is a ceilng above the character
            //m_Anim.SetBool("Ceiling", ceiling);
            m_SquishEnabled = crouch; // player state should only be set once it is sure what we want to do.
            if (!crouch && ceiling && m_HasSquishUpgrade) // if trying to stand up and there is a ceiling above
            {
                crouch = true;  // remain squished
                jump = false;   // set jump to false, incase trying to jump
            }

            m_Anim.SetBool("Squish", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }

            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground") && !crouch)
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                m_CanDoubleJump = true;
            }  
        }

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = playerGraphics.localScale;
            theScale.x *= -1; 

            playerGraphics.localScale = theScale;   
        }

        private Vector3 Lerp(Vector3 start, Vector3 end, float percent)
        {
            return (1 - percent) * start + (percent * end);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            //set it up in a switch so we can test all the upgrades we want
           /* switch (other.gameObject.name.ToString()) //looks for the names we give our upgrades
            {
                case "DoubleJumpIcon":
                    m_HasDoubleJumpUpgrade = true;
                    other.gameObject.SetActive(false);
                    break;

                default:
                    break;
            }
            */
        }
    }
}
