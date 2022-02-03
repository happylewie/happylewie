using UnityEngine;
using System.Collections;

public class introDialogEnd : MonoBehaviour {

	public GameObject stateMachine;
	public int checkForEventNumber;
	public Camera animationFocus;
//	private GameObject player; 

	// Use this for initialization
	void Start () {
		
		stateMachine = GameObject.Find ("StateMachine");
//		player = GameObject.Find ("Jeanette");

		stateMachine.GetComponent<ManageState> ().dialogState = 8;
		stateMachine.GetComponent<ManageState> ().state = 2;

	}
	
	// Update is called once per frame
	void Update () {

		if (stateMachine.GetComponent<ManageState> ().onOffSwitches [checkForEventNumber].onOff == true) {

//			player.GetComponent<Animator>().SetBool("sit",false);
			animationFocus.gameObject.SetActive(false);
			stateMachine.GetComponent<ManageState> ().onOffSwitches [checkForEventNumber].onOff = false;
			stateMachine.GetComponent<ManageState> ().state = 0;


		}
		
	}
	
}
