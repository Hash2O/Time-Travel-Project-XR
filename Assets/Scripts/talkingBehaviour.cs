using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talkingBehaviour : StateMachineBehaviour
{
    [SerializeField]
    private float _timeUntilTalk;

    [SerializeField]
    private int _numberOfTalkAnimations;

    private bool _isTalking;
    private float _idleTime;
    private int _talkingAnimation;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetIdle();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_isTalking == false)
        {
            _idleTime += Time.deltaTime;

            if (_idleTime > _timeUntilTalk && stateInfo.normalizedTime % 1 < 0.02f)
            {
                _isTalking = true;
                _talkingAnimation = Random.Range(1, _numberOfTalkAnimations + 1);
                _talkingAnimation = _talkingAnimation * 2 - 1;

                animator.SetFloat("talkingAnimations", _talkingAnimation - 1);
            }
        }
        else if (stateInfo.normalizedTime % 1 > 0.98)
        {
            ResetIdle();
        }

        animator.SetFloat("talkingAnimations", _talkingAnimation, 0.2f, Time.deltaTime);

    }

    private void ResetIdle()
    {
        if (_isTalking)
        {
            _talkingAnimation--;
        }

        _isTalking = false;
        _idleTime = 0;
    }
}
