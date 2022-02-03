using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAnimationModifier : MonoBehaviour
{
    public GameObject stateMachine;
    public Animator characterAnimator;
    public int eventToWatch;
    public string animationVariable;
    
    // Update is called once per frame
    void Update()
    {
        if (stateMachine.GetComponent<ManageState>().onOffSwitches[eventToWatch].onOff)
        {
            characterAnimator.SetBool(animationVariable,true);
        }
    }
}
