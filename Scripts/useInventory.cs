using UnityEngine;
using System.Collections;

public class useInventory : MonoBehaviour {

	private GameObject stateMachine;
	public bool inUse;

	// Use this for initialization
	void Start () {
	
		stateMachine = GameObject.Find ("StateMachine");

	}
	
	// Update is called once per frame
	void Update () {
	

	}

	void OnMouseDown () {

		stateMachine.GetComponent<ManageState> ().state = 3;

	}
}
