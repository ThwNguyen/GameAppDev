using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoolBehavior : StateMachineBehaviour
{
    public string boolName;
    public bool upDateOnStateMachine;
    public bool upDateOnState;
    public bool valueOnEnter;
    public bool valueOnExit;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (upDateOnState) ;
        animator.SetBool(boolName, valueOnEnter);
    }

    ////  OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    //  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //  {

    //  }

    //   OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (upDateOnState) ;
        animator.SetBool(boolName, valueOnExit);
        
    }


    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (upDateOnStateMachine)
            animator.SetBool(boolName, valueOnEnter);
    }



    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (upDateOnStateMachine)
            animator.SetBool(boolName, valueOnExit);
    }
}
