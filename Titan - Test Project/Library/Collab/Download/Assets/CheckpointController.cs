using UnityEngine;
using System.Collections;

public class CheckpointController : MonoBehaviour {
    //Stores current checkpoint
    //Tracks list of checkpoints
   
    private GameObject[] checkpointList;
    private GameObject currentActiveCheckpoint;
    private GameObject player;

    public string targetName = "CharacterRobotBoy";
    public string checkpointTag = "Checkpoint";

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
        player = GameObject.Find(targetName);
    }
        
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentActiveCheckpoint)
                // Move PC to current checkpoint
                player.gameObject.transform.position = currentActiveCheckpoint.gameObject.GetComponentsInChildren<Transform>()[1].position;
            // Could also reset health, ammo and stuff from here.
            return;
        }
	}

    public void UpdateCheckpoints(GameObject check)
    {
        if(mode == Mode.Regular) { 
            // Check if the checkpoint I just walked through is inactive or not
            if (check.gameObject.GetComponent<Checkpoint>().status != Checkpoint.State.Active)
            { // is inactive
              // set every point that is active to Used
              // set this point to active
              // Set any other checkpoints to used
                foreach (GameObject cp in checkpointList)
                {
                    if (cp.GetComponent<Checkpoint>().status != Checkpoint.State.Inactive)
                    {
                        cp.GetComponent<Checkpoint>().status = Checkpoint.State.Used;
                        cp.GetComponent<Checkpoint>().ChangeColor();
                    }
                }
                // Set this checkpoint as active
                check.gameObject.GetComponent<Checkpoint>().status = Checkpoint.State.Active;
                currentActiveCheckpoint = check;
            }
        }
    }
}
