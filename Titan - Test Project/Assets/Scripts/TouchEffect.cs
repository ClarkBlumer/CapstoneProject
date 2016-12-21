using UnityEngine;
using System.Collections;

public class TouchEffect : MonoBehaviour
{
    public bool touchingEffectsEnabled = false;
    public float touchingDamage = 0;
    public Vector2 touchingForce = new Vector2();

    public bool impactEffectsEnabled = false;
    public float impactDamage = 0;
    public Vector2 impactForce = new Vector2();

    public bool reflectForceRightLeft = true;//if target to the left, flip x force direction
    public bool reflectForceUpDown = false;//if target below, flip y force direction

    public float constantSpeedTime = 0.0f;
    public Vector2 constantSpeedOnHit = new Vector2();
    public Vector2 forceAfterSpeed = new Vector2();

    public string targetTag = null;

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(gameObject.name + "Entering collision with " + collision.gameObject.name);
        if (impactEffectsEnabled)
        {
            GameObject target = collision.gameObject;
            ApplyEffect(target, impactDamage, impactForce);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (touchingEffectsEnabled)
        {
            GameObject target = collision.gameObject;
            if (targetTag == null || targetTag == "" || targetTag == target.tag)
                ApplyEffect(target, Time.deltaTime * touchingDamage, touchingForce);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log(gameObject.name + "Entering collision with " + collider.gameObject.name);
        if (impactEffectsEnabled)
        {
            GameObject target = collider.gameObject;
            ApplyEffect(target, impactDamage, impactForce);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (touchingEffectsEnabled)
        {
            GameObject target = collider.gameObject;
            ApplyEffect(target, Time.deltaTime * touchingDamage, touchingForce);
        }
    }

    void ApplyEffect(GameObject target, float amountDamage, Vector2 force)
    {
        //do nothing if wrong tag
        if (targetTag == null || targetTag == "" || targetTag == target.tag)
        {
            //apply damage
            Health targetHealth = target.GetComponent<Health>();
            if (targetHealth != null && amountDamage != 0)
            {
                //Debug.Log(target.name + " takes " + amountDamage + " damage!");
                targetHealth.currentHealth -= amountDamage;
            }
            Rigidbody2D targetRBody = target.GetComponent<Rigidbody2D>();
            if (targetRBody != null && !targetRBody.isKinematic && force != null && (force.x != 0 || force.y != 0))
            {
                Vector2 forceToUse = new Vector2(force.x, force.y);
                //force flipping
                if (reflectForceUpDown || reflectForceRightLeft)
                {
                    Vector2 heading = target.transform.position - gameObject.transform.position;
                    if (reflectForceRightLeft && heading.x < 0 && force.x != 0)
                        forceToUse.x = -forceToUse.x; //reflect right to left
                    if (reflectForceUpDown && heading.y < 0 && force.y != 0)
                        forceToUse.y = -forceToUse.y; //reflect up to down
                }
                
                //apply force
                Debug.Log("Applying force x: " + forceToUse.x + ", y: " + forceToUse.y + " to " + targetRBody.ToString());
                targetRBody.AddForce(forceToUse, ForceMode2D.Force);

                //initial speed
                Debug.Log("SPEED TIME");
                if (constantSpeedTime > 0 && (constantSpeedOnHit.x != 0 || constantSpeedOnHit.y != 0))
                {
                    Debug.Log("HACKER VOICE I'M IN");
                    Vector2 newSpeed = new Vector2(
                        Mathf.Sign(forceToUse.x) * constantSpeedOnHit.x,
                        Mathf.Sign(forceToUse.y) * constantSpeedOnHit.y);
                    VelocityOverTime velocityOverTime = targetRBody.gameObject.AddComponent<VelocityOverTime>();
                    Debug.Log("LOOK AT THIS: " + velocityOverTime.ToString());
                    velocityOverTime.timeRemaining = constantSpeedTime;
                    velocityOverTime.newVelocity = newSpeed;
                    velocityOverTime.overrideWithZero = false;
                    velocityOverTime.forceAfter = new Vector2(
                        Mathf.Sign(forceToUse.x) * forceAfterSpeed.x,
                        Mathf.Sign(forceToUse.y) * forceAfterSpeed.y);
                }
            }
        }

    }
}
