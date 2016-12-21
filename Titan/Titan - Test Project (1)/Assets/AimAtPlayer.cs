using UnityEngine;
using System.Collections;

public class AimAtPlayer : MonoBehaviour {

    public float rotationOffset = 0;

    public bool enforceMinMax = false;
    public bool enforcedRangeLeft = false;
    public float rotationMin = 0;
    public float rotationMax = 0;

    private float prevRotationZ = 0.0f;
    
    public float maxAimRotation = 10;

    public string targetName;
    public bool weaponFlip = true;
    public bool leftMode = false;

    // Use this for initialization
    void Start()
    {
        prevRotationZ = transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        //find target
        GameObject player = GameObject.Find(targetName);
        if (player == null) return;

        //get angle
        Vector3 difference = player.transform.position - transform.position; //difference of our target's position and our aimer's position
        difference.Normalize(); //normalized.

        //Finds the angle of the origin point and the difference location. Then converts it to degrees to work with
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg + rotationOffset;

        //enforce max rotation speed
        float rotationThisFrame = maxAimRotation * Time.deltaTime;
        //find shortest direction
        if ((prevRotationZ >= 0 && (rotationZ > prevRotationZ || rotationZ < prevRotationZ - 180))
            || prevRotationZ < 0 && !(rotationZ < prevRotationZ || rotationZ > prevRotationZ + 180))
        {//positive direction
            if(prevRotationZ + rotationThisFrame > 180 && prevRotationZ + rotationThisFrame - 360 < rotationZ)
            {//goes over +/- boundary, exceeds limit
                rotationZ = prevRotationZ + rotationThisFrame - 360;
            }
            else if(rotationZ > prevRotationZ + rotationThisFrame)
            {//exceeds limit
                rotationZ = prevRotationZ + rotationThisFrame;
            }
        }
        else
        {//negative direction
            if (prevRotationZ - rotationThisFrame < 180 && prevRotationZ - rotationThisFrame + 360 > rotationZ)
            {//goes over +/- boundary, exceeds limit
                rotationZ = prevRotationZ - rotationThisFrame + 360;
            }
            else if (rotationZ < prevRotationZ - rotationThisFrame)
            {//exceeds limit
                rotationZ = prevRotationZ - rotationThisFrame;
            }
        }


        //enforce min/max
        if (enforceMinMax)
        {
            if (enforcedRangeLeft)
            {// bounds are to left
                if (rotationZ > rotationMin)
                    rotationZ = rotationMin;
                else if (rotationZ < rotationMax)
                    rotationZ = rotationMax;
            }
            else
            {// bounds are to right
                if (rotationZ < rotationMin)
                    rotationZ = rotationMin;
                else if (rotationZ > rotationMax)
                    rotationZ = rotationMax;
            }
        }

        prevRotationZ = rotationZ;

        //check weaponflip
        float rotateWeapon = 0f;
        if (weaponFlip) {
            if (rotationZ > 90f)
            {
                rotateWeapon = 180f;
                rotationZ = 180 - rotationZ;
            }
            if (rotationZ < -90f)
            {
                rotateWeapon = 180f;
                rotationZ = -180 - rotationZ;
            }
        }

        //leftmode
        Vector3 temp = transform.localScale;
        if ((leftMode && temp.x > 0) || (!leftMode && temp.x < 0))
        {
            temp.x = -temp.x;
            transform.localScale = temp;
        }

        //transform.rotation = Quaternion.Euler(rotateWeapon, rotateWeapon, rotationZ);
        transform.eulerAngles = new Vector3(0, rotateWeapon, rotationZ);
    }
    
}
