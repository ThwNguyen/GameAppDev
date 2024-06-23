using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemove : StateMachineBehaviour
{
    Color startColor;
    SpriteRenderer spriteRenderer;
    GameObject objToRemove;
    private float timeElapsed = 0.5f;
    [SerializeField]
    private float fadeTime = 0.9f;
    public float fadeDelay = 0.0f;
    private float fadeDelayElapsed = 0.0f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0f;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        objToRemove = animator.gameObject;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (fadeDelay > fadeDelayElapsed)
        {
            fadeDelayElapsed += Time.deltaTime;
        }

        else
        {
            timeElapsed += Time.deltaTime;
            float newAlpha = startColor.a * (timeElapsed / fadeTime);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            if (timeElapsed > fadeTime)
            {
                objToRemove.SetActive(false);//
                Debug.Log("die");

            }
        }

    }


}
