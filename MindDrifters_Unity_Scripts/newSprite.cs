using UnityEngine;
using System.Collections;

public class newSprite : MonoBehaviour {

	public int eventToWatch;
	public Sprite spriteToChangeTo;
	private GameObject stateMachine;

	// Use this for initialization
	void Start () {
	
		stateMachine = GameObject.Find ("StateMachine");

	}
	
	// Update is called once per frame
	void Update () {
	
		if (stateMachine.GetComponent<ManageState> ().onOffSwitches [eventToWatch].onOff) {
		
			transform.GetComponent<SpriteRenderer>().sprite = spriteToChangeTo;
			Destroy (this);

		}
	}
}
