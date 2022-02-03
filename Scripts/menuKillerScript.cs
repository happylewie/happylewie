using UnityEngine;
using System.Collections;

public class menuKillerScript : MonoBehaviour {

	private GameObject stateManager;
	private GameObject menuIsOpen;

	private bool bubbleDelay;

	// Use this for initialization
	void Start () {
	
		stateManager = GameObject.Find ("StateMachine");
		menuIsOpen = GameObject.Find ("Menu");
	}

	IEnumerator waitForIt(){
		
		bubbleDelay = true;
		yield return new WaitForSeconds(.02f);
		bubbleDelay = false;
		
	}

	void Update() {
		if (menuIsOpen.GetComponent<childMenu>().Menu_Open == false && transform.GetComponent<Collider>().enabled == true) {
			transform.GetComponent<Collider>().enabled = false;		
		}
	}

	public void OnMouseDown(){

		if (stateManager.GetComponent<ManageState> ().state == 1 && bubbleDelay == false) {
			transform.GetComponent<Collider>().enabled = false;
			stateManager.GetComponent<ManageState> ().state = 0;
		}

	}

}
