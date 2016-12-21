using UnityEngine;
using System.Collections;
using System;

public class Weapon : MonoBehaviour {

    public float fireRate; // 0 is single burst weapon
    public LayerMask whatGetsHit; // Lets us designate what the bullets can hit
    public GameObject bulletPrefab;
    public float effectSpawnRate = 10; //How many bullets spawn

    public AudioClip shootAudio;
    //public AudioClip reloadAudio;

    private float timeToFire;
    private Transform firePoint;
    private float timeToSpawnBullet;

    public float initialForceMult = 0.0f;

    public AmmoBelt ammoBelt;
    public int ammoPouch = -1;//to differentiate diff kinds of ammo; values less than 0 are 'no ammo' type.
    public float shootCost = 0;//ammo cost to shoot weapon, if uses ammo

    public bool playerWeapon = true;

    // Use this for initialization
    void Awake ()
    {
        firePoint = transform.FindChild("FirePoint");
        if(firePoint == null){ Debug.LogError("No FirePoint found!\n"); }
        //init ammoBelt.
        foundAmmoBelt();
	}

    private bool foundAmmoBelt()
    {
        if (ammoBelt == null)
        {
            ammoBelt = GetComponentInParent<AmmoBelt>();
            if (ammoBelt == null) return false;
        }
        return true;
    }

    // Update is called once per frame
    void Update ()
    {
        if (playerWeapon)
        {
            if (fireRate == 0) //single burst
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    tryShoot();
                }
            }
            else //fireRate != 0 automatic fire
            {
                if (Input.GetButton("Fire1") && Time.time > timeToFire)
                {
                    timeToFire = Time.time + 1 / fireRate;
                    tryShoot();
                }
            }
        }
	}

    public void tryShoot()
    {
        //Gets the moust position based on Screen to world.  Gets the position relative to the world, rather than the screen to account for different resolutions
        /*
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatGetsHit); //Raycast(origin, direction, distance, layermask)
                
        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.green); //Stretch forever! green cause - will show with gizmos turned on

        if (hit.collider != null) //if we hit something
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red); //red for hit, duh - This will show up with Gizmos enabled
            //Debug.Log("We hit " + hit.collider.name + " and did " + weaponDamage + " damage!\n");
        }
        */
        bool ammoCheck;
        if (shootCost == 0 || ammoPouch < 0 || !foundAmmoBelt()) ammoCheck = true;//don't need ammo
        else ammoCheck = ammoBelt.changeAmmo(-shootCost, ammoPouch);//attempt to deduct shootCost from our ammo type; get feedback.
        //Debug.Log("Can shoot: " + ammoCheck + " (" + -shootCost + ", " + ammoPouch + "), ammobelt? " + (ammoBelt != null));
        if (ammoCheck && Time.time >= timeToSpawnBullet)
        {
            Shoot();
            timeToSpawnBullet = Time.time + (1 / effectSpawnRate);
        }
    }

    //spawns the simple bullets
    void Shoot()
    {
        if (shootAudio && shootAudio.loadState == AudioDataLoadState.Loaded)
        {
            AudioManager.instance.PlaySound(shootAudio, transform.position);
        }
        else
        {
            Debug.Log(this.gameObject.ToString() + " : is Missing shoot audio");
        }

        GameObject bullet = Instantiate(this.bulletPrefab, firePoint.position, firePoint.rotation) as GameObject;
        //add force if able to
        Rigidbody2D bulletPhysics = bullet.GetComponent<Rigidbody2D>();
        if (bulletPhysics != null && initialForceMult != 0.0f)
        {
            bulletPhysics.AddForce(bullet.transform.right * initialForceMult, ForceMode2D.Impulse);
        }

    }

    

}
