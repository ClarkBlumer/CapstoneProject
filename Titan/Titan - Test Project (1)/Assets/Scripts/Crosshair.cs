using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

    public Texture2D crosshair;
    public Rect pos;
    private bool hasGunEquipped = false;
    
	// Use this for initialization
	void Start () {
        UnityEngine.Cursor.visible = false;
        if (gameObject.GetComponentInChildren<Weapon>())
            hasGunEquipped = true;
    }
    
    void Update()
    {
        if (gameObject.GetComponentInChildren<Weapon>())
        {
            hasGunEquipped = true;
        }
        else
        {
            hasGunEquipped = false;
        }
        
    }

    void OnGUI()
    {
        if (hasGunEquipped)
        {
            // draw on current mouse position
            float xMin = Screen.width - (Screen.width - Input.mousePosition.x) - (crosshair.width / 2);
            float yMin = (Screen.height - Input.mousePosition.y) - (crosshair.height / 2);
            GUI.DrawTexture(new Rect(xMin, yMin, crosshair.width, crosshair.height), crosshair);
        }
    }
}
