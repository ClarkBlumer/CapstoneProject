using UnityEngine;
using System.Collections;

public class EnemyWeaponShooter : MonoBehaviour {

    public bool active = false;

    public float shootTimeAvg = 3.0f;
    public float shootTimeDeviation = 1.0f;

    private float timeToShoot = 1.0f;
    public Weapon weapon;

    // Use this for initialization
    void Start () {
        getWeapon();
    }
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            if(timeToShoot > 0)
            {
                timeToShoot -= Time.deltaTime;
            } else
            {
                timeToShoot = shootTimeAvg + Random.Range(-shootTimeDeviation, shootTimeDeviation);
                //Debug.Log(timeToShoot + " seconds until " + gameObject.name + " fires again.");
                if (getWeapon())
                {
                    weapon.tryShoot();
                }
            }
        }
	}

    bool getWeapon()
    {
        if (weapon != null) return true;
        weapon = gameObject.GetComponent<Weapon>();
        return weapon != null;
    }
}
