using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class CheckpointController : MonoBehaviour {
    //Stores current checkpoint
    //Tracks list of checkpoints
   
    private GameObject[] checkpointList;
    private GameObject currentActiveCheckpoint;
    private GameObject player;
 

    public string targetName = "CharacterRobotBoy";
    public string checkpointTag = "Checkpoint";
    public GameObject prefab;
    
    //public GameObject defaultCheckpoint;

    public enum Mode
    {
        Regular, Locked
    };

    public Mode mode;
    //Preforms the logic which determines which cp (if any is to be set as current)
    void Awake()
    {
        //Get list of checkpoints
        checkpointList = GameObject.FindGameObjectsWithTag(checkpointTag);

        foreach (var cp in checkpointList)
        {
            var cps = cp.GetComponent<Checkpoint>();
            if (cps.checkpointName.Equals("Home"))
            {
                UpdateCheckpoints(cp);
            }
        }
        player = GameObject.Find(targetName);
    }
        
    public void Respawn()
    {
        if (player && currentActiveCheckpoint)
        {
            // Move PC to current checkpoint
            player.transform.position = currentActiveCheckpoint.GetComponent<Checkpoint>().spawn.transform.position;
            player.GetComponent<Health>().currentHealth = 100;
            player.SetActive(true);
            return;
        }
    }

    public void UpdateCheckpoints(GameObject check/*, GameObject player*/)
    {
        if(mode == Mode.Regular) { 
            // Check if the checkpoint I just walked through is inactive or not
            if (check.gameObject.GetComponent<Checkpoint>().Status != Checkpoint.State.Active)
            { // is inactive or other
              // set every point that is active to Used
              // set this point to active
              // Set any other checkpoints to used
                foreach (GameObject cp in checkpointList)
                {
                    if (cp.GetComponent<Checkpoint>().Status != Checkpoint.State.Inactive)
                    {
                        cp.GetComponent<Checkpoint>().Status = Checkpoint.State.Used;
                        cp.GetComponent<Checkpoint>().ChangeColor();
                    }
                }
                // Set this checkpoint as active
                check.gameObject.GetComponent<Checkpoint>().Status = Checkpoint.State.Active;
                currentActiveCheckpoint = check;

            }
        }
    }
}
