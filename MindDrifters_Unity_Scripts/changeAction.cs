using UnityEngine;
using System.Collections;

public class changeAction : MonoBehaviour {

	public int eventNumber;
	public string actionToChange;
	public bool stayTrue;
	public bool changeDialog;
	public int dialogNumber;
	private GameObject stateMachine;

	// Use this for initialization
	void Start () {
		stateMachine = GameObject.Find ("StateMachine");
	}
	
	// Update is called once per frame
	void Update () {

		if (stateMachine.GetComponent<ManageState> ().onOffSwitches [eventNumber].onOff == true) {

			if (actionToChange == "hear") { transform.GetComponent<Object_Interactor>().hear = !transform.GetComponent<Object_Interactor>().hear;
				if (changeDialog) { transform.GetComponent<Object_Interactor>().hearDialog = dialogNumber; }}
			else if (actionToChange == "look") { transform.GetComponent<Object_Interactor>().look = !transform.GetComponent<Object_Interactor>().look; 
				if (changeDialog) { transform.GetComponent<Object_Interactor>().lookDialog = dialogNumber; }}
			else if (actionToChange == "open") { transform.GetComponent<Object_Interactor>().openDoor = !transform.GetComponent<Object_Interactor>().openDoor; 				
				if (changeDialog) { transform.GetComponent<Object_Interactor>().openDoorDialog = dialogNumber; }}
			else if (actionToChange == "touch") { transform.GetComponent<Object_Interactor>().touch = !transform.GetComponent<Object_Interactor>().touch; 				
				if (changeDialog) { transform.GetComponent<Object_Interactor>().touchDialog = dialogNumber; }}
			else if (actionToChange == "take") { transform.GetComponent<Object_Interactor>().take = !transform.GetComponent<Object_Interactor>().take; 				
				if (changeDialog) { transform.GetComponent<Object_Interactor>().takeDialog = dialogNumber; }}
			else if (actionToChange == "taste") { transform.GetComponent<Object_Interactor>().taste = !transform.GetComponent<Object_Interactor>().taste; 				
				if (changeDialog) { transform.GetComponent<Object_Interactor>().tasteDialog = dialogNumber; }}
			else if (actionToChange == "talk") { transform.GetComponent<Object_Interactor>().talk = !transform.GetComponent<Object_Interactor>().talk; 				
				if (changeDialog) { transform.GetComponent<Object_Interactor>().talkDialog = dialogNumber; transform.GetComponent<Object_Interactor> ().talk = true;}}
			else if (actionToChange == "smell") { transform.GetComponent<Object_Interactor>().smell = !transform.GetComponent<Object_Interactor>().smell; 				
				if (changeDialog) { transform.GetComponent<Object_Interactor>().smellDialog = dialogNumber; }}

			Destroy(this); //Exterminate!!
		}

	}
}
