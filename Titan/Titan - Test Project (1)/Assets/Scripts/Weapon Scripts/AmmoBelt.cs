using UnityEngine;
using System.Collections;
using System;

public class AmmoBelt : MonoBehaviour {

    [SerializeField]
    private AmmoPouch[] ammoPouches;


    // Use this for initialization
    void Start() {
        foreach (AmmoPouch aP in ammoPouches)
        {
            //check that init value is not illegal
            if (aP.currentAmmo < 0 || aP.currentAmmo > aP.maxAmmo) aP.currentAmmo = aP.maxAmmo;
        }
    }

    // Update is called once per frame
    void Update() {
        foreach (AmmoPouch aP in ammoPouches)
        {
            //recharge ammo
            if (aP.rechargeRate != 0 && (aP.currentAmmo < aP.maxAmmo)) aP.currentAmmo += (aP.rechargeRate * Time.deltaTime);
            //enforce max ammo
            if (aP.currentAmmo > aP.maxAmmo) aP.currentAmmo = aP.maxAmmo;
            //minimum 0 ammo
            if (aP.currentAmmo < 0) aP.currentAmmo = 0;
        }
    }

    public void addMaxAmmo(float ammoMaxIncrease, int ammoType)
    {
        //types below 0 don't use ammo; no change for ammoAmount == 0.
        if (ammoType < 0 || ammoMaxIncrease == 0 || ammoType >= ammoPouches.Length) return;
        //increase max ammo in this pouch
        ammoPouches[ammoType].maxAmmo += ammoMaxIncrease;
        //add this amount to ammo storage
        changeAmmo(ammoMaxIncrease, ammoType);
    }



    public bool changeAmmo(float ammoAmount, int ammoType)
    {
        //types below 0 don't use ammo; no change for ammoAmount == 0.
        if (ammoType < 0 || ammoAmount == 0) return true;
        //not enough ammo, or ammo type isn't init'd.
        if (ammoPouches[ammoType].currentAmmo < -ammoAmount || ammoType >= ammoPouches.Length) return false;
        //otherwise, have enough ammo
        ammoPouches[ammoType].currentAmmo += ammoAmount;
        return true;
    }

    private void resizeAmmoPouches(int newSize)
    {
        Array.Resize<AmmoPouch>(ref ammoPouches, newSize);
    }

    public int addAmmoPouch(float newPouchMax, float newRechargeRate, float initialAmount)
    {
        int newPouchID = ammoPouches.Length;
        resizeAmmoPouches(ammoPouches.Length + 1);
        //init pouch
        ammoPouches[newPouchID].maxAmmo = newPouchMax;
        ammoPouches[newPouchID].rechargeRate = newRechargeRate;
        changeAmmo(initialAmount, newPouchID);
        //give ammo type
        return newPouchID;
    }

    [Serializable]
    private class AmmoPouch {
        public float currentAmmo;
        public float maxAmmo;
        public float rechargeRate;//ammo per second recharge. negative for ammo that decreases when idle.
    }
}
