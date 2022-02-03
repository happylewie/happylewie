using UnityEngine;
using System.Collections;

public class ifThisThenThat : MonoBehaviour {

	public int ifThis;
	public int thenThat;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.GetComponent<ManageState>().onOffSwitches[ifThis].onOff) transform.GetComponent<ManageState>().onOffSwitches[thenThat].onOff = true ;
	}
}
