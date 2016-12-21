using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;
        public float damping = 0.8f;
        public float lookAheadFactorX = 5;
        public float lookAheadFactorY = 6;
        public float lookUpToDownRatio = -0.5f;
        public float lookAheadReturnSpeed = 4;
        public float lookAheadMoveThreshold = 0.1f;
        public float fastModeThreshold = 2.9f;
        public float fastModeTimeMult = 0.8f;
        public Vector3 targetOffset = new Vector3(0, 3.0F, 0);

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;
        private float nextTimeToSearch = 0f;

        void Awake()
        {
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            if (target == null)
            {
                target = transform.parent;
            }
        }
        
        // Use this for initialization
        private void Start()
        {
            
        }

        // Update is called once per frame
        private void Update()
        {
            //This will look for the player if the target is null
            if(target == null)
            {
                FindPlayer();

            }

            //only update if there's a target.............
            if (target != null)
            {
                Vector3 targetPosition = target.position + targetOffset;
                // only update lookahead pos if accelerating or changed direction
                float xMoveDelta = (targetPosition - m_LastTargetPosition).x;
                float yMoveDelta = (targetPosition - m_LastTargetPosition).y;

                bool updateLookAheadTargetX = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;
                bool updateLookAheadTargetY = Mathf.Abs(yMoveDelta) > lookAheadMoveThreshold;

                if (updateLookAheadTargetX || updateLookAheadTargetY)
                {
                    Vector3 m_LookAheadPosX = new Vector3();
                    Vector3 m_LookAheadPosY = new Vector3();
                    if (updateLookAheadTargetY)
                    {
                        m_LookAheadPosY = lookAheadFactorY * Vector3.up * Mathf.Sign(yMoveDelta);
                        if (yMoveDelta > 0)
                        {
                            m_LookAheadPosY *= lookUpToDownRatio;
                        }
                    }
                    if (updateLookAheadTargetX)
                    {
                        m_LookAheadPosX = lookAheadFactorX * Vector3.right * Mathf.Sign(xMoveDelta);
                    }
                    m_LookAheadPos = m_LookAheadPosX + m_LookAheadPosY;
                }
                else
                {
                    m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
                }

                Vector3 aheadTargetPos = targetPosition + m_LookAheadPos + Vector3.forward * m_OffsetZ;

                //speed up if lagging behind!
                float currentDamping = damping;
                float offset = (aheadTargetPos - transform.position).magnitude;
                if (offset != 0 && offset > fastModeThreshold)
                {
                    currentDamping = currentDamping * ((fastModeThreshold/offset)*fastModeTimeMult);
                }

                Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, currentDamping);

                transform.position = newPos;

                m_LastTargetPosition = targetPosition;
            }
        }

        //Will look for player
        void FindPlayer()
        {
            if(nextTimeToSearch <= Time.time)
            {
                GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
                if(searchResult != null)
                {
                    target = searchResult.transform;
                }
                nextTimeToSearch = Time.time + 0.5f; 
            }
        }
    }
}
