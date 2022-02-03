using UnityEngine;
using System.Collections;

public class Scene01_animatino_foodTrap : MonoBehaviour {

	public GameObject objectToMakeAppear;
	public int checkForEventNumber;
	private GameObject stateMachine;

	// Use this for initialization
	void Start () {
	
		stateMachine = GameObject.Find ("StateMachine");

	}
	
	// Update is called once per frame
	void Update () {
	
		if (stateMachine.GetComponent<ManageState> ().onOffSwitches [checkForEventNumber].onOff == true) {
			stateMachine.GetComponent<ManageState>().state = 2;
			transform.GetComponent<Animator>().SetBool("trapOpen",true);
			stateMachine.GetComponent<ManageState> ().onOffSwitches [checkForEventNumber].onOff = false;
		}

	}

	void foodTrayIn(){
		objectToMakeAppear.SetActive (true);
	}
}
