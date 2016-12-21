using UnityEngine;
using System.Collections;

public class ReleasableChild : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnParentAboutToBeDestroyed()
    {
        Debug.Log(name + " is releasing itself");
        transform.SetParent(null);
        if (transform.parent == null)
        {
            Debug.Log(name + " succeeded!");
        }
    }

}
