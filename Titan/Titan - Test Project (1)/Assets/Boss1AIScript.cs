using UnityEngine;
using System.Collections;

public class Boss1AIScript : MonoBehaviour {
    

    public Vector2 aimInitial = new Vector2();

    public bool activate = false;

    public float restTimeAvg = 10.0f;
    public float restTimeDeviation = 2.0f;
    public float armingTimeAvg = 2.0f;
    public float armingTimeDeviation = 0.0f;
    public float lazerTimeAvg = 2.0f;
    public float lazerTimeDeviation = 0.0f;
    public float cooldownTimeAvg = 2.0f;
    public float cooldownTimeDeviation = 0.0f;

    private float countDown = 0.0f;
    public GameObject eyeShield;
    public GameObject eyeWeapon;
    public Health health;

    private AttackState currentState;

    // Use this for initialization
    void Start () {
        health = GetComponent<Health>();
        currentState = AttackState.Idle;
        eyeShield.SetActive(true);
        eyeWeapon.SetActive(false);
        health.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        //decrement countdown
        if (countDown > 0)
            countDown -= Time.deltaTime;

        //behavior depends on state
        switch (currentState)
        {
            case AttackState.Resting:
                //state: shield up
                //if countdown ends, drop shield and transition to Arming state
                if (countDown <= 0)
                {
                    eyeShield.SetActive(false);
                    health.enabled = true;
                    currentState = AttackState.Arming;
                    countDown = armingTimeAvg + Random.Range(-armingTimeDeviation, armingTimeDeviation);
                }
                break;
            case AttackState.Arming:
                //state: shield down, before lazer
                //if countdown ends, start lazer and transition to Lazering state
                if (countDown <= 0)
                {
                    eyeWeapon.SetActive(true);
                    eyeWeapon.transform.eulerAngles = new Vector3(0, 0, 180);
                    currentState = AttackState.Lazering;
                    countDown = lazerTimeAvg + Random.Range(-lazerTimeDeviation, lazerTimeDeviation);
                }
                break;
            case AttackState.Lazering:
                //state: lazer firing
                //if countdown ends, end lazer and transition to CoolingDown state
                if (countDown <= 0)
                {
                    eyeWeapon.SetActive(false);
                    currentState = AttackState.CoolingDown;
                    countDown = cooldownTimeAvg + Random.Range(-cooldownTimeDeviation, cooldownTimeDeviation);
                }
                break;
            case AttackState.CoolingDown:
                //state: lazer over, shield down
                //if countdown ends, reactivate shield and transition to Resting state
                if (countDown <= 0)
                {
                    eyeShield.SetActive(true);
                    health.enabled = false;
                    currentState = AttackState.Resting;
                    countDown = restTimeAvg + Random.Range(-restTimeDeviation, restTimeDeviation);
                }
                break;
            default:
                //state: Idle (or broken)
                //if activate is true, transition to Resting state
                if (activate)
                    currentState = AttackState.Resting;
                break;

        }
    }

    private enum AttackState { Idle, Resting, Arming, Lazering, CoolingDown }
}
