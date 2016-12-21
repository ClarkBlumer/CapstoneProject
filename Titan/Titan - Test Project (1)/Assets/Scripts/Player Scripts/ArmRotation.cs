using UnityEngine;
using System.Collections;

public class ArmRotation : MonoBehaviour {

    public int rotationOffset = 90;

	// Update is called once per frame
	void Update ()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; //difference of our mouse position and our character position
        difference.Normalize(); // x + y + z = 1 for the vector.  Let's us use smaller numbers

        //Finds the angle of the origin point and the difference location. Then converts it to degrees to work with
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg + rotationOffset;
        
        float rotateWeapon = 0f;
        if (rotationZ > 90f) {
            rotateWeapon = 180f;
            rotationZ = 180 - rotationZ;
        }
        if (rotationZ < -90f)
        {
            rotateWeapon = 180f;
            rotationZ = -180 - rotationZ;
        }

        //transform.rotation = Quaternion.Euler(rotateWeapon, rotateWeapon, rotationZ);
        transform.eulerAngles = new Vector3(0, rotateWeapon, rotationZ);
	}
}
