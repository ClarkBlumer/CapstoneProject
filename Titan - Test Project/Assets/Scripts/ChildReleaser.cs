using UnityEngine;
using System.Collections;

public class ChildReleaser : MonoBehaviour {

	// Use this for initialization
	void Start() { }

    // Update is called once per frame
    void Update() { }
    
    void OnDestroy() {
        //Debug.Log(gameObject.name + " is being destroyed; Releasing applicable children.");
        foreach (Transform child in transform)
        {
            //Debug.Log("Attempting to release " + child.name);
            ReleasableChild releasableChild = child.GetComponent<ReleasableChild>();
            if(releasableChild != null)
            {
                releasableChild.OnParentAboutToBeDestroyed();
            }
        }
    }
}
