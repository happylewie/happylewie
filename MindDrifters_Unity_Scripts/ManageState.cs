using UnityEngine;
using System.Collections;

[System.Serializable]
public class onOffVariables {
	
	public float valueNumber;
	public string description;
	public bool onOff;

}

public class ManageState : MonoBehaviour {


	public int state = 0;
	//state 0 = game On
	//state 1 = menu On
	//state 2 = animation "cutscene"
	//state 3 = Inventory selected
	//state 4 = Inventory Used
	//state 5 = Player position locked
	//state 6 = Move player without player interaction

	public int dialogState = -1;
	public GameObject dialogBox;
	[Header("Disable Dialog Box object first")]
	public bool startSceneWithDialog;
	public int startDialog;

	public onOffVariables[] onOffSwitches;
	// Use this for initialization
	void Start () {

		if (startSceneWithDialog == true) dialogState = startDialog;
		// Screen.SetResolution (850, 400, false);
		// Screen.showCursor = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (dialogState == -1) {
			dialogBox.SetActive (false);
		}
		
		else {
			dialogBox.SetActive(true);
		}
	}
}
