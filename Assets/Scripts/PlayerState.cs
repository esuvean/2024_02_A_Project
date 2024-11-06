using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public abstract class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected PlayerController playerController;
    protected PlayerAnimationManager animationManager;

    public PlayerState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.playerController = stateMachine.playerController;
        this.animationManager=stateMachine.GetComponent<PlayerAnimationManager>();
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }    
    public virtual void FixedUpdate() { } 


    protected void CheckTransitions() 
    {
        if (playerController.isGrounded())
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                stateMachine.TransitionToState(new JumpingState(stateMachine));
            }
            else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                stateMachine.TransitionToState(new MoveingState(stateMachine));
            }
            else
            {
                stateMachine.TransitionToState(new IdleState(stateMachine));
            }
        }
        else
        {
            if (playerController.GetVerticalVelocity() > 0)
            {
                stateMachine.TransitionToState(new JumpingState(stateMachine));
            }
            else
            {
                stateMachine.TransitionToState(new FallingState(stateMachine));
            }
        }
    }
}

public class IdleState : PlayerState
{
    public IdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    public override void Update()
    {
        CheckTransitions();
    }
}

public class MoveingState : PlayerState
{
    private bool isRunning;
    public MoveingState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    public override void Update()
    {
        isRunning = Input.GetKeyUp(KeyCode.LeftShift);
        CheckTransitions();
    }
    public override void FixedUpdate() 
    {
        playerController.HandleMovement();

    }
}

public class JumpingState : PlayerState
{
    public JumpingState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    public override void Update()
    {
        CheckTransitions();
    }
    public override void FixedUpdate()
    {
        playerController.HandleMovement();

    }
}
public class FallingState : PlayerState
{
    public FallingState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    public override void Update()
    {
        CheckTransitions();
    }
    public override void FixedUpdate()
    {
        playerController.HandleMovement();

    }
}