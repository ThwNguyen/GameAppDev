using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setFloatBehaviour : StateMachineBehaviour
{
    public string floatName;
    public bool upDateOnStateMachineEnter, upDateOnStateMachineExit;
    public bool upDateOnStateEnter, upDateOnStateExit;


    public float valueOnEnter;
    public float valueOnExit;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (upDateOnStateEnter) ;
        animator.SetFloat(floatName, valueOnEnter);
    }

    //   OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (upDateOnStateExit) ;
        animator.SetFloat(floatName, valueOnExit);
    }


    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (upDateOnStateMachineEnter)
            animator.SetFloat(floatName, valueOnEnter);
    }



    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (upDateOnStateMachineExit)
            animator.SetFloat(floatName, valueOnExit);
    }
}
