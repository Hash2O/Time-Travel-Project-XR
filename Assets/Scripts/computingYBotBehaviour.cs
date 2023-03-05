using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class computingYBotBehaviour : StateMachineBehaviour
{
    [SerializeField]
    private float _timeUntilNextTask;

    [SerializeField]
    private int _numberOfTasks;

    private bool _isComputing;
    private float _idleTime;
    private int _taskAnimation;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetIdle();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_isComputing == false)
        {
            _idleTime += Time.deltaTime;

            if (_idleTime > _timeUntilNextTask && stateInfo.normalizedTime % 1 < 0.02f)
            {
                _isComputing = true;
                _taskAnimation = Random.Range(1, _numberOfTasks + 1);
                _taskAnimation = _taskAnimation * 2 - 1;

                animator.SetFloat("newOccupation", _taskAnimation - 1);
            }
        }
        else if (stateInfo.normalizedTime % 1 > 0.98)
        {
            ResetIdle();
        }

        animator.SetFloat("newOccupation", _taskAnimation, 0.2f, Time.deltaTime);

    }

    private void ResetIdle()
    {
        if (_isComputing)
        {
            _taskAnimation--;
        }

        _isComputing = false;
        _idleTime = 0;
    }
}
