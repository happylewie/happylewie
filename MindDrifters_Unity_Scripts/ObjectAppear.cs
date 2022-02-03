using UnityEngine;
using System.Collections;

public class ObjectAppear : MonoBehaviour {

	public int eventNumber;
	public GameObject toAppear;

	// Update is called once per frame
	void Update () {
	
		if (transform.GetComponent<ManageState> ().onOffSwitches [eventNumber].onOff == true) {
			toAppear.gameObject.SetActive(true);
			Destroy(this);
		}

	}
}
