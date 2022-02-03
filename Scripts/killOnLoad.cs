using UnityEngine;
using System.Collections;

public class killOnLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.gameObject.SetActive (false);
		Destroy (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
