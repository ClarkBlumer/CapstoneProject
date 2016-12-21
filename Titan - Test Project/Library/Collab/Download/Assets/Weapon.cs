using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public float fireRate = 0; // 0 is single burst weapon
    public float weaponDamage = 10;
    public LayerMask whatGetsHit; // Lets us designate what the bullets can hit
    public Transform bulletTrailPrefab;
    public float effectSpawnRate = 10; //How many bullets spawn

    private float timeToFire = 0;
    private Transform firePoint;
    private float timeToSpawnBullet = 0;
    


	// Use this for initialization
	void Awake ()
    {
        firePoint = transform.FindChild("FirePoint");
        if(firePoint == null){ Debug.LogError("No FirePoint found!\n"); }
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        if (fireRate == 0) //single burst
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else //fireRate != 0 automatic fire
        {
            if(Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
	}

    void Shoot()
    {
        Debug.Log("Pew pew!\n");

        //Gets the moust position based on Screen to world.  Gets the position relative to the world, rather than the screen to account for different resolutions
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);


        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);

        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatGetsHit); //Raycast(origin, direction, distance, layermask)
        if (Time.time >= timeToSpawnBullet)
        {
            Effect();
            timeToSpawnBullet = Time.time + (1 / effectSpawnRate);
        }
        //Effect();
        //Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.green); //Stretch forever! green cause 

        if (hit.collider != null) //if we hit something
        {
            //Debug.DrawLine(firePointPosition, hit.point, Color.red); //red for hit, duh
            Debug.Log("We hit " + hit.collider.name + " and did " + weaponDamage + " damage!\n");
        }
    }

    //spawns the simple bullets
    void Effect()
    {
        Instantiate(bulletTrailPrefab, firePoint.position, firePoint.rotation);
    }
}
