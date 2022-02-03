using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class childMenu : MonoBehaviour {

	public GameObject Menu_Hear;
	public GameObject Menu_Look;
	public GameObject Menu_OpenDoor;
	public GameObject Menu_Touch;
	public GameObject Menu_Take;
	public Sprite takeImage;
	public GameObject Menu_Taste;
	public GameObject Menu_Talk;
	public GameObject Menu_Smell;

	public bool Menu_Open;
	public GameObject stateManager;


	void Start(){

		stateManager = GameObject.Find ("StateMachine");

	}

	public void animDone(){
		
		transform.GetComponent<Animator>().SetBool("menu_open", false);
		
	}

	void Update(){

		if (Menu_Open == true && stateManager.GetComponent<ManageState> ().state == 0) {

			CloseMe ();		
		}
	}

	IEnumerator waitForIt(){
		
		yield return new WaitForSeconds(.02f);
		Menu_Open = false;

	}

	void CloseMe(){

		Menu_Hear.SetActive(false);
		Menu_Look.SetActive(false);
		Menu_OpenDoor.SetActive(false);
		Menu_Touch.SetActive(false);
		if (Menu_Take.activeSelf == true) {GameObject.Find("Menu_take").GetComponent<Image>().sprite = takeImage;}
		Menu_Take.SetActive(false);
		Menu_Taste.SetActive(false);
		Menu_Smell.SetActive(false);
		Menu_Talk.SetActive(false);
		StartCoroutine (waitForIt ());
	
	}

}
