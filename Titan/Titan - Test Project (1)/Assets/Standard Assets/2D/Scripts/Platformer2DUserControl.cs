using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        //public bool m_CanDoubleJump = false;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }



        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
                if (m_Jump)
                {
                    if (m_Character.m_Grounded)
                    {
                        m_Character.m_Rigidbody2D.velocity = new Vector2(m_Character.m_Rigidbody2D.velocity.x, 0);
                        //m_Character.m_Rigidbody2D.AddForce(new Vector2(0, m_Character.m_JumpForce));
                        m_Character.m_CanDoubleJump = true;
                    }
                    else
                    {
                        if (m_Character.m_CanDoubleJump && m_Character.m_HasDoubleJumpUpgrade)
                        {
                            m_Character.m_CanDoubleJump = false;
                            m_Character.m_Rigidbody2D.velocity = new Vector2(m_Character.m_Rigidbody2D.velocity.x, 0);
                            m_Character.m_Rigidbody2D.AddForce(new Vector2(0, m_Character.m_JumpForce));
                        }
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            // Read the inputs.

            //Check if player is allowed to crouch
            bool crouch = false;
            if (m_Character.m_HasSquishUpgrade)
                 crouch = Input.GetKey(KeyCode.LeftShift);
            
            //Get L|R movement mag
            float h = CrossPlatformInputManager.GetAxis("Horizontal");

            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
