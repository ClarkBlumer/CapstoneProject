using UnityEngine;
using System.Collections;

public class CreateOnDestroy : MonoBehaviour {

    public GameObject successorPrefab;
    public Vector3 offset = new Vector2();

    public double createChance = 1;
    public int numberToCreate = 1;

    private bool isQuitting = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnApplicationQuit()//is always called before everything is OnDestroy()-d
    {
        isQuitting = true;
    }

    void OnDestroy()
    {
        if (!isQuitting)//application is closing, DON'T MAKE MORE OBJECTS
        {
            for (int i = 0; i < numberToCreate; i++)
            {
                //check chance to spawn successor against random number generator.
                if (createChance <= 0 || (createChance < 1 && Random.value > createChance)) return;
                //passed random test   
                //give an instance of childToGive to target
                GameObject successor = Instantiate(successorPrefab);
                successor.transform.position = gameObject.transform.position + offset;
            }
        }
    }
}
