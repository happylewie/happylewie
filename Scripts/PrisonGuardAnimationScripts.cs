using UnityEngine;

public class PrisonGuardAnimationScripts : MonoBehaviour {

	public GameObject stateMachine;
	public int checkForEventNumber;
	public bool focus; 
	public Camera animationFocus;
	private GameObject player;

	// Use this for initialization
	void Start () {
	
		stateMachine = GameObject.Find ("StateMachine");
		player = GameObject.Find ("Jeanette");

	}
	
	// Update is called once per frame
	void Update () {
	
		if (stateMachine.GetComponent<ManageState> ().onOffSwitches [checkForEventNumber].onOff == true) {

			if (player.transform.localPosition.x > -8 && player.transform.localPosition.x < -5.7 && player.transform.localPosition.y > -4) {
				
				player.transform.localPosition = new Vector3(-5.7f,player.transform.localPosition.y,player.transform.localPosition.z);
				
			}

			stateMachine.GetComponent<ManageState> ().state = 2;
			gameObject.GetComponent<Animator> ().SetBool ("Angry", true);
			stateMachine.GetComponent<ManageState> ().onOffSwitches [checkForEventNumber].onOff = false;
			if (focus) animationFocus.gameObject.SetActive(true);

		}

	}

	void ChangeCamera(){
		if (focus) animationFocus.gameObject.SetActive (false);
	}

	void AnimationEnded() {

		gameObject.GetComponent<Animator> ().SetBool ("Angry", false);
		stateMachine.GetComponent<ManageState> ().dialogState = 6;
		stateMachine.GetComponent<ManageState> ().state = 0;
	
	}
}
