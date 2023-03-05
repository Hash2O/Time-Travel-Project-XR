using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapoeiraBehaviour : StateMachineBehaviour
{
    [SerializeField]
    private float _timeUntilStrike;

    [SerializeField]
    private int _numberOfCapoeiraAnimations;

    private bool _isStriking;
    private float _idleTime;
    private int _capoeiraAnimation;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetIdle();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_isStriking == false)
        {
            _idleTime += Time.deltaTime;

            if (_idleTime > _timeUntilStrike && stateInfo.normalizedTime % 1 < 0.02f)
            {
                _isStriking = true;
                _capoeiraAnimation = Random.Range(1, _numberOfCapoeiraAnimations + 1);
                _capoeiraAnimation = _capoeiraAnimation * 2 - 1;

                animator.SetFloat("capoeiraPosition", _capoeiraAnimation - 1);
            }
        }
        else if (stateInfo.normalizedTime % 1 > 0.98)
        {
            ResetIdle();
        }

        animator.SetFloat("capoeiraPosition", _capoeiraAnimation, 0.2f, Time.deltaTime);

    }

    private void ResetIdle()
    {
        if (_isStriking)
        {
            _capoeiraAnimation--;
        }

        _isStriking = false;
        _idleTime = 0;
    }
}
