using UnityEngine;
using System.Collections;

public class CreepShooter : MonoBehaviour
{

    public GameObject bullet;

    public float minTime = 1f;
    public float maxTime = 2f;
    public AudioClip shootAudio;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Attack()); // Starts spider shooter attack
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        if (shootAudio && shootAudio.loadState == AudioDataLoadState.Loaded)
        {
            AudioManager.instance.PlaySound(shootAudio, transform.position);
        }
        Instantiate(bullet, transform.position, Quaternion.identity); //TODO: Instantiate the bullet behind the spider
        StartCoroutine(Attack()); //
    }
}
