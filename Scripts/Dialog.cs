using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class dialogOptions
{
	public string dialogAnswer;
	public int answerDuration;
}

[System.Serializable]
public class dialogData
{
	[TextArea] public string dialogText;
	public int dialogDuration;
	public Transform bubblePosition;
	public int actionNumber;
	public AudioClip characterTone;
	public bool doNotClearNext;
	public Animator hasCharacterTalking;
//	public Color textColor;
//	public int dialogOrder;
	public dialogOptions[] dialogOptionList;
}

public class Dialog : MonoBehaviour {
	
	public dialogData[] dialogDataList;
	private int dialogValue;
	public bool isLoaded = false;
	private bool bubbleDelay = false;
	private Vector3 bubblePosFixed;
	private float depassement;
	private float delay;
	public bool firstLoad;
	public AudioSource dialogSound;
	private bool playDialogSound = true;
	private bool animateMouth = false;
	private bool yellowEmphasis;
	public GameObject[] answersLabels;
	
	public Text textParameters;
	public bool CameraIs3D;
	
	private GameObject stateMachine;

	// KEEP THERE, MIGHT NEED LATER
	/* IEnumerator BubbleDelayRoutine(){
		
		bubbleDelay = true;
		yield return new WaitForSeconds(.2f);
		bubbleDelay = false;
		
	} */

	IEnumerator TypeText()
	{
		// IF CHARACTER HAS A SOUND WHEN TALKING, DEFINE HERE (BEEP DIALOG)
		// TODO: Tweak to allow voice acting
		if (dialogDataList[dialogValue].characterTone != null)
		{
			playDialogSound = true;
			dialogSound.clip = dialogDataList[dialogValue].characterTone;
		}
		else
		{
			playDialogSound = false;
		}

		// SMALL DELAY BEFORE DISPLAYING TEXT, TO SMOOTH THE DIALOG
		yield return new WaitForSeconds(0.5f);

		// IF CHARACTER HAS ANIMATION AVAILABLE, OTHERWISE SKIP
		if (dialogDataList[dialogValue].hasCharacterTalking != null)
		{
			animateMouth = true;
			dialogDataList[dialogValue].hasCharacterTalking.SetBool("Talking", true);
		}

		// DISPLAY TEXT LETTER BY LETTER
		foreach (var letter in dialogDataList[dialogValue].dialogText.ToCharArray())
		{

			// WILL TOGGLE YELLOW ON TEXT WHEN SEEING @ CHARACTER. WILL *NOT* DISPLAY @ CHARACTERS. WORKS AS A TOGGLE
			if (letter.Equals('@'))
			{
				yellowEmphasis = !yellowEmphasis;
				continue;
			}

			if (yellowEmphasis)
			{
				string tempColorize = "<color=yellow>" + letter + "</color>";
				textParameters.text += tempColorize;
			}
			else
			{
				textParameters.text += letter;
				if (playDialogSound)
				{
					dialogSound.Play();
				}
			}

			yield return new WaitForSeconds(0.05f);
		}

		// FINISH TALKING ANIMATION
		if (animateMouth)
		{
			animateMouth = false;
			dialogDataList[dialogValue].hasCharacterTalking.SetBool("Talking", false);
		}

		// IF ANSWER SELECTION IS AVAILABLE, DISPLAY SAID OPTIONS
		if (dialogDataList[dialogValue].dialogOptionList.Length > 0)
		{
			for (var i = 0; i <= dialogDataList[dialogValue].dialogOptionList.Length -1; i++)
			{
				yield return new WaitForSeconds(0.02f);
				answersLabels[i].SetActive(true);
				answersLabels[i].GetComponent<Text>().text = ""; // TODO: OPTIMIZE

				foreach (var letter in dialogDataList[dialogValue].dialogOptionList[i].dialogAnswer.ToCharArray())
				{
					answersLabels[i].GetComponent<Text>().text += letter; // TODO: OPTIMIZE
					yield return new WaitForSeconds(0.02f);
				}

				answersLabels[i].GetComponent<Choices>().nextDialog =
					dialogDataList[dialogValue].dialogOptionList[i].answerDuration;
			}
		}
		isLoaded = true;
	}

	void Start()
	{
		stateMachine = GameObject.Find("StateMachine");
		textParameters = transform.GetComponent<Text>();
	}

	void Update(){
		
		if (isLoaded){
			if (Input.GetMouseButtonDown(0) && bubbleDelay == false && dialogDataList[dialogValue].dialogOptionList.Length == 0){
				gameObject.SetActive(false);
			}
		}
	}
	
	void OnDisable (){
		
		isLoaded = false;

		// MAKE SURE ALL OPTIONS ARE DISABLED
		foreach (var possibleAnswers in answersLabels)
		{
			possibleAnswers.SetActive(false);
		}
		
		// dialogValue = stateMachine.GetComponent<ManageState>().dialogState; //Why was it here? 

		if (!firstLoad) {
		
						if (dialogDataList [dialogValue].actionNumber != -1) {
								// switch On
								stateMachine.GetComponent<ManageState> ().onOffSwitches [dialogDataList [dialogValue].actionNumber].onOff = true;
						}

						if (dialogValue != -1 && dialogDataList[dialogValue].dialogOptionList.Length == 0) {
								stateMachine.GetComponent<ManageState> ().dialogState = dialogDataList [dialogValue].dialogDuration;
						}
		
		}
		firstLoad = false;
	}
	
	// OnEnable is called when SetActive(true) for the current object
	void OnEnable ()
	{

		stateMachine = GameObject.Find ("StateMachine");
		dialogValue = stateMachine.GetComponent<ManageState>().dialogState;
		// StartCoroutine(BubbleDelayRoutine());
		
		if (!dialogDataList[dialogValue].doNotClearNext) // continues on previous text
		{
			textParameters.text = ""; // Clear previous text
		}

		
		if (dialogValue != -1){

			if (CameraIs3D) {
				transform.position = new Vector3 (dialogDataList[dialogValue].bubblePosition.position.x,dialogDataList[dialogValue].bubblePosition.position.y, 100);
			}
			else
			{
				transform.position = dialogDataList[dialogValue].bubblePosition.position;
			}


			StartCoroutine(TypeText());

		}
	}
}
