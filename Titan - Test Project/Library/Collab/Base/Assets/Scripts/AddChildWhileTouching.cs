using UnityEngine;
using System.Collections;

public class AddChildren : MonoBehaviour {

    public GameObject parent;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D target)
    {
        Vector3 scale = target.transform.lossyScale;
        //Debug.Log(scale);
        
        target.transform.SetParent(this.transform);
        target.transform.localPosition = target.transform.InverseTransformPoint(target.transform.position);
        target.transform.localScale = scale;
        //Debug.Log("Scale character after childed:"+target.transform.localScale);

        //Debug.Log("Scale of platform: " + this.gameObject.transform.localScale);
        //target.transform.localScale = new Vector3(scale.x/parent.transform.localScale.x, scale.y/parent.transform.localScale.y);
    }

    void OnCollisionExit2D(Collision2D target)
    {
        //Vector3 pos = target.transform.position;
        //Vector3 scale = target.transform.lossyScale;
        //Vector3 scale = target.transform.localScale;
        //target.transform.SetParent(null, false);
        //target.transform.localScale = scale;// new Vector3(scale.x* parent.transform.localScale.x, scale.y* parent.transform.localScale.y);
       
        //target.transform.position = pos;
        //target.transform.localScale = scale;
    }
}
