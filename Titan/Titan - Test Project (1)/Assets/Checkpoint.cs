using UnityEngine;
using System.Collections;
[RequireComponent(typeof(SpriteRenderer))]
public class Checkpoint : MonoBehaviour {

    private SpriteRenderer sprite;
    private CheckpointController cp;
    
    internal Transform spawn;

    public enum State { Inactive, Active, Used, Locked };
    public string checkpointName = "";

    [SerializeField]
    private State status;
    
    [HideInInspector]
    public State Status {
        get
        {
            return status;
        }
        set
        {
            status = value;
            ChangeColor();
        }
    }

    void Awake()
    {
        cp = gameObject.GetComponentInParent<CheckpointController>();
        spawn = gameObject.GetComponentInChildren<Transform>();
        
        //Get this obj's sprite
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
        ChangeColor();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            cp.UpdateCheckpoints(this.gameObject);
            ChangeColor();
        }
    }

    public void ChangeColor()
    {
        if (sprite)
        {
            if (Status == State.Inactive)
            {
                sprite.color = Color.red;
            }
            else if (Status == State.Active)
            {
                sprite.color = Color.green;
            }
            else if (Status == State.Used)
            {
                sprite.color = Color.blue;
            }
        }
    }
    //Check for triggering
    //if triggered report to CheckpointController

    public override string ToString()
    {
        return "Checkpoint!";
        //return base.ToString();
    }
}
