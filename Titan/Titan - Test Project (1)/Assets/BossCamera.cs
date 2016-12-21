using UnityEngine;
using System.Collections;

public class BossCamera : MonoBehaviour {

   /* private float originalCameraSize;
    private float currentCameraSize;
    private Camera mainCamera;
    private bool zoomOut = false;

    public string target = "";
    [Range(0.1f,1000f)] public float targetCameraSize = 10.0f;

    // Use this for initialization
	void Start () {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        originalCameraSize = mainCamera.GetComponent<Camera>().orthographicSize;
        currentCameraSize = originalCameraSize;
	}
	
	// Update is called once per frame
	void Update () {
	    if(zoomOut && !(Mathf.Abs(currentCameraSize-targetCameraSize) < 0.1f)) //Need to be zoomed out but isn't there yet
        {
            currentCameraSize = Mathf.Lerp(originalCameraSize, targetCameraSize, )
        }
	}

    void OnTriggerEnter2D(Collider2D target)
    {
        zoomOut = true;
    }

    void OnTriggerExit2D(Collider2D target)
    {
        zoomOut = false;
    }

    void OnTriggerStay2D(Collider2D target)
    {

    }*/
}
