using UnityEngine;

public class ChangeCamera : MonoBehaviour
{


        public GameObject stateMachine;
        public int checkForEventNumber;
        public GameObject newCameraObject;
        public GameObject oldCameraObject;
        public Camera newCamera;
        private GameObject _activeCanvas;

        // Use this for initialization
        void Start () {
	
            stateMachine = GameObject.Find ("StateMachine");
            _activeCanvas = GameObject.Find("Canvas");

        }
        
        // Update is called once per frame
        void Update () {
	
            if (stateMachine.GetComponent<ManageState>().onOffSwitches[checkForEventNumber].onOff)
            {
                oldCameraObject.SetActive(false);
                newCameraObject.SetActive(true);
                _activeCanvas.GetComponent<Canvas>().worldCamera = newCamera;
                stateMachine.GetComponent<ManageState> ().onOffSwitches [checkForEventNumber].onOff = false;
            }

        }

    }

