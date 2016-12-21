using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Health: MonoBehaviour {

    public float currentHealth = 100;
    public float maxHealth = 100;
    public float minHealth = 0;
    public bool showHealth = true;
    public bool hasHealthBar = false;
    public float deathDelay = 0f;
    public GameObject bar;
    public Text healthText;
    public AudioClip deathAudio;
    public GameObject respawnUI;
    private BarScript barScript;
    

    private Animator anim;  
    private bool requireRespawn = false;
    
    // Use this for initialization
    void Awake ()
    {
        if (hasHealthBar && bar)
        {
            barScript = bar.GetComponent<BarScript>();
        }
        else if(hasHealthBar)
        {
            throw new System.ArgumentException("Health bar reference cannot be null");
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth < minHealth)
        {
            currentHealth = minHealth;
        }

        if (hasHealthBar && bar)
        {
            bar.GetComponent<BarScript>().UpdateValue(currentHealth);
        }
        
        if (showHealth && healthText)
            healthText.text = currentHealth.ToString();
        if (currentHealth <= 0)
        {
            if (deathAudio && deathAudio.loadState == AudioDataLoadState.Loaded)
            {
                AudioManager.instance.PlaySound(deathAudio, transform.position);
            }

            if (gameObject.tag == "Player")
            {
                //Deactivate playercharacter
                gameObject.SetActive(false);
                //Activate respawn menu
                if (respawnUI != null) respawnUI.SetActive(true);
            }
            else Destroy(gameObject);

        }
	}

    IEnumerator delayedDestroy()
    {
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }

    public void doDamage(float amountDamage)
    {
        if (this.enabled)
        {
            Debug.Log(amountDamage);
            currentHealth -= amountDamage;
        }
    }
} // Health
