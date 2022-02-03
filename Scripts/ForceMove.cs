using UnityEngine;

public class ForceMove : MonoBehaviour {

	public Transform character;
	public float destinationX;
	public float destinationY;
	
	public void ForceMoveMethod(){

		character.GetComponent<ClickToMove2>().forceMoveCharacter(destinationX,destinationY);

	}

}
