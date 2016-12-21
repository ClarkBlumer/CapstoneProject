using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour {
    private Transform platform;

    private Transform currentPoint;

    public float moveSpeed;

    public Transform[] points;
    public Vector3 velocity;
    private int pointSelection = 0;

    // Use this for initialization
    void Start()
    {
        if(points.Length > 0)
        {
            currentPoint = points[pointSelection];
        }else
        {
            currentPoint = null;
        }
            
        platform = gameObject.GetComponentInChildren<Transform>();
    }

    void Update()
    {
        if (platform != null && points != null && currentPoint != null)
        {
            //Debug.Log("platform is not null");
            if (platform.position == currentPoint.position)
            {
                if (++pointSelection >= points.Length)
                {
                    // If we are here that means we need to wrap around
                    pointSelection = 0;
                    currentPoint = points[0];
                }
                else if (points[pointSelection] != null)
                {
                    currentPoint = points[pointSelection];
                }
            }
            platform.position = Vector3.MoveTowards(platform.position, currentPoint.position, Time.deltaTime * moveSpeed);
        }
    }
}
