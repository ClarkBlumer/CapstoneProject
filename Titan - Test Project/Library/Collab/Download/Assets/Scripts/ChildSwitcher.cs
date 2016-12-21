using UnityEngine;
using System.Collections;
using System;

public class ChildSwitcher : MonoBehaviour {

    string switchAxis = "Mouse ScrollWheel";
    int currentChild;
    private int prevChild;
    double switchDelay = 0.5F;
    private double switchWait = 0F;

	// Use this for initialization
	void Start () {
        currentChild = 0;
        prevChild = -1;
        switchChild();
    }
	
	// Update is called once per frame
	void Update () {
        if (switchWait > 0.0F)//not ready to switch yet
        {
            switchWait -= Time.deltaTime;//decrease switch delay
        }
        else
        {//ready to switch
            float axisInput = Input.GetAxis(switchAxis);
            if (axisInput < 0)//down 1 weapon
            {
                currentChild--;
                switchChild();
            } else if (axisInput > 0)//up one weapon
            {
                currentChild++;
                switchChild();
            }//else, do nothing
        }
    }

    private void switchChild()
    {
        Debug.Log("attempting to switch to weapon # " + currentChild);
        //attempt to switch to currentWeapon-indexed weapon from children
        //get children objects that are switchable children
        SwitchableChild[] switchableChildren = gameObject.GetComponentsInChildren<SwitchableChild>(true);
        //has children??
        int numSwitchableChildren = switchableChildren.Length;
        Debug.Log("# of weapons: " + numSwitchableChildren);
        if (numSwitchableChildren < 1) return;
        //test if currentChild is in bounds
        if (currentChild >= numSwitchableChildren) currentChild = 0;
        if (currentChild < 0) currentChild = numSwitchableChildren-1;
        Debug.Log("attempting to switch to actual weapon # " + currentChild + " from weapon " + prevChild);
        if (currentChild == prevChild) return; //no switch
        //do switch
        //disable all
        for (int i = 0; i < numSwitchableChildren; i++)
            switchableChildren[i].gameObject.SetActive(false);
        //activate currentChild
        switchableChildren[currentChild].gameObject.SetActive(true);
        //finish up, set delay, prev wep
        prevChild = currentChild;
        switchWait = switchDelay;
    }
}
