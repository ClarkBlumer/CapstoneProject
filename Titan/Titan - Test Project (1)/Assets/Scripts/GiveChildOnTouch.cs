using UnityEngine;
using System.Collections;

public class GiveChildOnTouch : MonoBehaviour {

    public string targetTag = "Player";
    public string childTarget = null;
    public GameObject childToGive;
    public bool giveActive = true;
    public Vector2 positionOffset = new Vector2();
    public Vector3 rotationOffset = new Vector3();

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (targetTag == "" || targetTag == null || collider.gameObject.tag == targetTag)
        {
            //get target transform
            Transform target = collider.gameObject.transform;
            //set target transform to appropriate child, if findable
            if (childTarget != null && childTarget != "") {
                Transform tempTarget = target.Find(childTarget);
                if (tempTarget != null) target = tempTarget;
            }
            //give an instance of childToGive to target
            GameObject child = Instantiate(childToGive);
            child.transform.SetParent(target);
            child.transform.localPosition = positionOffset;
            child.transform.localRotation = Quaternion.Euler(rotationOffset);
            child.gameObject.SetActive(giveActive);
            Destroy(gameObject);
        }
    }
}
