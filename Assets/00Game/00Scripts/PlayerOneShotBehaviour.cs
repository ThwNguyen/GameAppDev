using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneShotBehaviour : StateMachineBehaviour
{
    public AudioClip soundToPlay;
    public float volume = 2f;

    public bool playOnEnter = true;
    public bool playOnExit = false;
    public bool playAfterDelay = false;
    public float playDelay = 0.25f;

    private float timeSinceEntered = 0;
    private bool hasDelayedSoundPlayed = false;

   
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnEnter)
        {
            PlayOneShotSound(animator.gameObject.transform.position);
        }
        timeSinceEntered = 0;
        hasDelayedSoundPlayed = false;
    }

   
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!hasDelayedSoundPlayed && playAfterDelay)
        {
            timeSinceEntered += Time.deltaTime;

            if (timeSinceEntered > playDelay)
            {
                PlayOneShotSound(animator.gameObject.transform.position);
                hasDelayedSoundPlayed = true;
            }
        }
    }

   
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnExit)
        {
            PlayOneShotSound(animator.gameObject.transform.position);
        }
    }

    private void PlayOneShotSound(Vector3 position)
    {
       // AudioSource.PlayClipAtPoint(soundToPlay, position, 100);
    }
}
