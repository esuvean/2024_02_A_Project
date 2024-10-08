using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState currentState;
    public PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        TransitionToState(new IdleState(this));
    }
    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }

    public void TransitionToState(PlayerState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
        Debug.Log($"Transitioned to State {newState.GetType().Name}");
    }
}
