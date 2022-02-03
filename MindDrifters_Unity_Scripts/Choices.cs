using UnityEngine;
using UnityEngine.UI;

public class Choices : MonoBehaviour
{

	public int nextDialog;
	public GameObject dialogBox;
	public GameObject stateMachine;

	void OnMouseEnter()
	{
		gameObject.transform.GetComponent<Text>().color = new Color32(255,0,0,255);
	}

	void OnMouseExit()
	{
		gameObject.transform.GetComponent<Text>().color = new Color32(255,255,255,255);
	}

	void OnMouseDown()
	{
		if (dialogBox.GetComponent<Dialog>().isLoaded)
		{
			stateMachine.GetComponent<ManageState>().dialogState = nextDialog;
			OnMouseExit(); //reset color
			dialogBox.SetActive(false);
		}
	}
}
