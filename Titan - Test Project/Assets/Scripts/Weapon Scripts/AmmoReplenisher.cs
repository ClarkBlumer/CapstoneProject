using UnityEngine;
using System.Collections;

public class AmmoReplenisher : MonoBehaviour {
    
    public string targetTag = "Player";
    
    public float amountAmmo = 0;
    public int ammoType = -1;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (amountAmmo != 0 && (targetTag == "" || targetTag == null || collider.gameObject.tag == targetTag))
        {
            AmmoBelt ammoBelt = collider.transform.FindChild("GunAnchor").GetComponent<AmmoBelt>();
            if (ammoBelt != null) ammoBelt.changeAmmo(amountAmmo, ammoType);
        }
    }
}
