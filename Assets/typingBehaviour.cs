using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class typingBehaviour : StateMachineBehaviour
{
    [SerializeField]
    private float _timeUntilWork;

    [SerializeField]
    private int _numberofTypingAnimations;

    private bool _isTyping;
    private float _idleTime;
    private int _typingAnimation;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetIdle();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_isTyping == false)
        {
            _idleTime += Time.deltaTime;

            if (_idleTime > _timeUntilWork && stateInfo.normalizedTime % 1 < 0.02f)
            {
                _isTyping = true;
                _typingAnimation = Random.Range(1, _numberofTypingAnimations + 1);
                _typingAnimation = _typingAnimation * 2 - 1;

                animator.SetFloat("workingAnimations", _typingAnimation - 1);
            }
        }
        else if (stateInfo.normalizedTime % 1 > 0.98)
        {
            ResetIdle();
        }

        animator.SetFloat("workingAnimations", _typingAnimation, 0.2f, Time.deltaTime);

    }

    private void ResetIdle()
    {
        if (_isTyping)
        {
            _typingAnimation--;
        }

        _isTyping = false;
        _idleTime = 0;
    }
}
