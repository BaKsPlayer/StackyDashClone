using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private BaseInput _input;
    public BaseInput Input => _input;

    [SerializeField] private Stack _stack;
    public Stack Stack => _stack;

    [SerializeField] private RoadBlock _startRoadElement;
    public RoadBlock CurrentBlock { get; private set; }

    public State CurrentState { get; private set; }
    public PlayerMovement Movement { get; private set; }

    [Header("States:")]
    [SerializeField] private State _idleState;
    [SerializeField] private State _movingState;

    public Vector2 MoveDirection { get; private set; }

    public UnityAction OnPlayerStopped;

    private void Awake()
    {
        Movement = GetComponent<PlayerMovement>();
        CurrentBlock = _startRoadElement;

        SetState(_idleState);
    }

    public void SetDirection(Vector2 direction)
    {
        MoveDirection = direction;
    }

    public void SetCurrentRoadBlock(RoadBlock block)
    {
        CurrentBlock = block;
    }

    private void Update()
    {
        CurrentState?.Update();
    }

    public void SetState(State state)
    {
        state ??= _idleState;

        CurrentState = Instantiate(state);
        CurrentState.SetPlayer(this);
        CurrentState.Init();
    }

    public void Stop()
    {
        SetState(_idleState);
        OnPlayerStopped?.Invoke();
    }
}
