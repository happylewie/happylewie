using UnityEngine;
using System.Collections;

public class menu_click : MonoBehaviour {

	public int dialogScript;

	// Use this for initialization
	void OnMouseDown(){
		GameObject.Find ("StateMachine").GetComponent<ManageState>().dialogState = dialogScript;
		GameObject.Find ("MenuKiller").GetComponent<menuKillerScript>().OnMouseDown();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
