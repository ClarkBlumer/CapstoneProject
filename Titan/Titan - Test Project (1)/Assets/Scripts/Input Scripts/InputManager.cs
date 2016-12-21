using UnityEngine;
using System.Collections;

public enum Buttons{
	Right,
	Left
}

public enum Condition{
	GreaterThan,
	LessThan
}

/* Class which records the state value of a particular axis in unity's
own input manager */
[System.Serializable] //Lets Unity know that the class instance objects can be edited in editor
public class InputAxisState{

	//All public properties are viewable in Unity (have to belong to serializable class)
	public string axisName;
	public float offValue;
	public Buttons button;
	public Condition condition;

	public bool value {	//A property: basically a property is a way to abstract access to fields
		get{
			var val = Input.GetAxis (axisName);

			switch (condition) {
			case Condition.GreaterThan:
				return val > offValue;
			case Condition.LessThan:
				return val < offValue;
			}
			//Default case
			return false;
		}
	}
}

public class InputManager : MonoBehaviour { //Serialzable bc MonoBehaviour is and InputManager inherits from it so it is as well

	//Array of object of type InputAxisState
	public InputAxisState[] inputs; //These will be created in editor, and come from Unity itselft

	public InputState inputState;   //By adding this, in the editor, we can place an obj which has
									//a InputState script attached to it

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach (var input in inputs) {
			inputState.SetButtonValue (input.button, input.value); //Passing the input state to InputState
		}
	}
}
