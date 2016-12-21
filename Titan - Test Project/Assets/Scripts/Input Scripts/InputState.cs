using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ButtonState{
	public bool value;
	public float holdTime = 0;

}

public class InputState : MonoBehaviour {

	private Dictionary<Buttons, ButtonState> buttonStates = new Dictionary<Buttons, ButtonState> (); /* Should only contain (Right,)(Left,)*/

	public void SetButtonValue(Buttons key, bool value){
		//Check to see if key exists, if not add it else return it's value
		if (!buttonStates.ContainsKey (key))
			buttonStates.Add (key, new ButtonState());
		var state = buttonStates [key]; //Get the state attached to a specific key

		//Check if button has been released
		if (state.value && !value) {
			Debug.Log ("Button " + key + " released after "+state.holdTime);
			state.holdTime = 0;
		} else if (state.value && value) {
			state.holdTime += Time.deltaTime;
			Debug.Log ("Button " + key + " down for"+state.holdTime);
		}
		
		state.value = value;

	}

	public bool GetButtonValue(Buttons key){
		if(buttonStates.ContainsKey(key))
			return buttonStates[key].value;
		else
			return false;
	}
}
