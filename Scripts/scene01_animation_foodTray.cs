using UnityEngine;
using System.Collections;

public class scene01_animation_foodTray : MonoBehaviour {

	public GameObject objectToAnimate;
	private GameObject stateMachine;

	// Use this for initialization
	void Start () {
		stateMachine = GameObject.Find ("StateMachine");	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void closeTrap(){
		objectToAnimate.GetComponent<Animator> ().SetBool ("trapOpen", false);
	}

	void animationEnded(){
		stateMachine.GetComponent<ManageState> ().state = 0;
	}
}
