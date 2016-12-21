using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health: MonoBehaviour {

    public float currentHealth = 100;
    public float maxHealth = 100;
    public float minHealth = 0;
    public bool showHealth = true;
    public float deathDelay = 0f;
    public BarScript bar;
    public Text healthText;
    public AudioClip deathAudio;
    public GameObject respawnUI;
    //public GameObject checkpointController;
    private Animator anim;  
    private bool requireRespawn = false;
    
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>(); // Added by: David Reese (9/26)
        if(bar)
            bar.MaxValue = maxHealth;
       // if (!checkpointController)
        //    checkpointController = GameObject.FindWithTag("CheckpointController");
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
        // Check if bar here
        if (bar != null)
            bar.Value = currentHealth;
        
        if (showHealth && healthText)
            healthText.text = currentHealth.ToString();
        if (currentHealth <= 0)
        {
            if (deathAudio && deathAudio.loadState == AudioDataLoadState.Loaded)
            {
                AudioManager.instance.PlaySound(deathAudio, transform.position);
            }
            //Deactivate playercharacter
            gameObject.SetActive(false);
            //Activate respawn menu
            respawnUI.SetActive(true);
            
            //checkpointController.GetComponent<CheckpointController>();
            //Debug.Log("Oh no, " + gameObject.name + " died!");
            //if (showHealth && healthText) healthText.text = "ded";

            // If there is an animator, playe death anim
            /*if (anim)   // Added all in loop by: David Reese (9/26)
             {
                 //Debug.Log("Start death animation...");
                 //this.gameObject.GetComponent<Collider2D>().enabled = false;
                 //anim.SetTrigger("Die");
                 //StartCoroutine(delayedDestroy());
                 //Destroy(this.gameObject);
             }
             else { Destroy(gameObject); }
             //Destroy(gameObject);
             */

        }
	}

    IEnumerator delayedDestroy()
    {
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }
  
} // Health
