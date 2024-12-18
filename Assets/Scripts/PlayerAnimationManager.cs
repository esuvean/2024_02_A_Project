using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public Animator animator;
    public PlayerStateMachine stateMachine;

    private const string PARAM_IS_MOVEING = "IsMoving";
    private const string PARAM_IS_RUNNING = "IsRunning";
    private const string PARAM_IS_JUMPING = "IsJumping";
    private const string PARAM_IS_FALLING = "IsFalling";
    private const string PARAM_IS_ATTACK_TRIGGER = "Attack";

    public void TriggerAttack()
    {
        animator.SetTrigger(PARAM_IS_ATTACK_TRIGGER);
    }

    private void ResetAIIBoolParameters()
    {
        animator.SetBool(PARAM_IS_MOVEING, false);
        animator.SetBool(PARAM_IS_RUNNING, false);
        animator.SetBool(PARAM_IS_JUMPING, false);
        animator.SetBool(PARAM_IS_FALLING, false);
    }

    public void Update()
    {
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (stateMachine.currentState != null)
        {
            ResetAIIBoolParameters();
            switch (stateMachine.currentState)
            {
                case IdleState:
                    break;

                case MoveingState:
                    animator.SetBool(PARAM_IS_MOVEING, true);
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        animator.SetBool(PARAM_IS_RUNNING, true);
                    }
                    break;
                case JumpingState:
                    animator.SetBool (PARAM_IS_JUMPING, true);
                    break;
                case FallingState:
                    animator.SetBool(PARAM_IS_FALLING, true);
                    break;
            }
        }
    }
}
