using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Object_Interactor : MonoBehaviour {

	public GameObject menu;

	public bool hear;
	public int hearDialog;
	public bool hearClose;
	public int hearCloseD;

	public bool look;
	public int lookDialog;
	public bool lookClose;
	public int lookCloseD;

	public bool openDoor;
	public bool doorIsLocked;
	public int openDoorDialog;
    public float unlockDoorAction;

	public bool touch;
	public int touchDialog;

	public bool take;
	public bool takeToUse;
	public Sprite useSprite;
	public int takeDialog;


	public bool taste;
	public int tasteDialog;

	public bool talk;
	public int talkDialog;

	public bool smell;
	public int smellDialog;

	public bool isClose;

	public int getCloserUseItem;
	public int useItemDialog;

	private bool bubbleDelay;
	private GameObject stateMachine;
	private GameObject menuKiller;

	void OnTriggerEnter(Collider other) {
		isClose = true;
	}

	void OnTriggerExit(Collider other){
		isClose = false;
	}

	void Start(){

		stateMachine = GameObject.Find ("StateMachine");
		menuKiller = GameObject.Find ("MenuKiller");
		bubbleDelay = false;

	}

	public void callMenu(){

		stateMachine.GetComponent<ManageState>().state = 1;
		menuKiller.transform.GetComponent<Collider>().enabled = true;


		menu.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, -3);
		if (menu.transform.localPosition.y < -125) menu.transform.localPosition = new Vector3 (menu.transform.localPosition.x, -125f, menu.transform.localPosition.z);
		menu.GetComponent<childMenu> ().Menu_Open = true;
		menu.GetComponent<Animator> ().SetBool ("menu_open", true);


		if (hear == true && menu.GetComponent<childMenu>().Menu_Hear.activeSelf==false) {
			menu.GetComponent<childMenu>().Menu_Hear.SetActive(true);
			if (hearClose && isClose) { GameObject.Find ("Menu_listen").GetComponent<menu_click>().dialogScript = hearCloseD; } else { GameObject.Find ("Menu_listen").GetComponent<menu_click>().dialogScript = hearDialog; }
		}
		if (hearClose && isClose && !hear) {
			menu.GetComponent<childMenu>().Menu_Hear.SetActive(true);
			GameObject.Find ("Menu_listen").GetComponent<menu_click>().dialogScript = hearCloseD;
		}
		if (look == true && menu.GetComponent<childMenu>().Menu_Look.activeSelf==false) {
			menu.GetComponent<childMenu>().Menu_Look.SetActive(true);
			if (lookClose && isClose) { GameObject.Find ("Menu_look").GetComponent<menu_click>().dialogScript = lookCloseD; } else { GameObject.Find ("Menu_look").GetComponent<menu_click>().dialogScript = lookDialog; }
		}
		if (isClose && openDoor == true && menu.GetComponent<childMenu>().Menu_OpenDoor.activeSelf==false) {
			menu.GetComponent<childMenu>().Menu_OpenDoor.SetActive(true);
			if (doorIsLocked) { GameObject.Find ("Menu_opendoor").GetComponent<menu_click>().dialogScript = openDoorDialog; }
		}
		if (isClose && touch == true && menu.GetComponent<childMenu>().Menu_Touch.activeSelf==false) {
			menu.GetComponent<childMenu>().Menu_Touch.SetActive(true);
			GameObject.Find ("Menu_touch").GetComponent<menu_click>().dialogScript = touchDialog;
		}
		if (isClose && take == true && menu.GetComponent<childMenu>().Menu_Take.activeSelf==false) {
			menu.GetComponent<childMenu>().Menu_Take.SetActive(true);
			GameObject.Find ("Menu_take").GetComponent<menu_click>().dialogScript = takeDialog;
			if (takeToUse) GameObject.Find("Menu_take").GetComponent<Image>().sprite = useSprite;
		}
		if (isClose && taste == true && menu.GetComponent<childMenu>().Menu_Taste.activeSelf==false) {
			menu.GetComponent<childMenu>().Menu_Taste.SetActive(true);
			GameObject.Find ("Menu_taste").GetComponent<menu_click>().dialogScript = tasteDialog;
		}
		if (isClose && smell == true && menu.GetComponent<childMenu>().Menu_Smell.activeSelf==false) {
			menu.GetComponent<childMenu>().Menu_Smell.SetActive(true);
			GameObject.Find ("Menu_smell").GetComponent<menu_click>().dialogScript = smellDialog;
		}
		if (isClose && talk == true && menu.GetComponent<childMenu>().Menu_Talk.activeSelf==false) {
			menu.GetComponent<childMenu>().Menu_Talk.SetActive(true);
			GameObject.Find ("Menu_talk").GetComponent<menu_click>().dialogScript = talkDialog;
		}


	}

	void OnMouseDown(){

		if (stateMachine.GetComponent<ManageState> ().state == 0 && GameObject.Find ("StateMachine").GetComponent<ManageState> ().dialogState == -1) {
			callMenu ();
		}
		else if (stateMachine.GetComponent<ManageState> ().state == 5 && GameObject.Find ("StateMachine").GetComponent<ManageState> ().dialogState == -1) {
			callMenu ();
		} 
		else if (stateMachine.GetComponent<ManageState> ().state == 1 && bubbleDelay == false) {
			GameObject.Find("MenuKiller").transform.GetComponent<Collider>().enabled = false;
			stateMachine.GetComponent<ManageState> ().state = 0;
		}
	}



}
