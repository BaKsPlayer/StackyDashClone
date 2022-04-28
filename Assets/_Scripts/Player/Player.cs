using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private BaseInput _input;
    public BaseInput Input => _input;

    [SerializeField] private Stack _stack;
    public Stack Stack => _stack;

    [SerializeField] private RoadElement _startRoadElement;
    public RoadElement CurrentRoadElement { get; private set; }

    public State CurrentState { get; private set; }
    public PlayerMovement Movement { get; private set; }

    [Header("States:")]
    [SerializeField] private State _idleState;
    [SerializeField] private State _movingState;

    public Vector2 MoveDirection { get; private set; }

    private void Awake()
    {
        Movement = GetComponent<PlayerMovement>();
        CurrentRoadElement = _startRoadElement;

        SetState(_idleState);
    }

    public void SetDirection(Vector2 direction)
    {
        MoveDirection = direction;
    }

    private void Update()
    {
        CurrentState?.Update();
    }

    public void SetState(State state)
    {
        if (state == null)
        {
            CurrentState = null;
            return;
        }

        CurrentState = Instantiate(state);
        CurrentState.SetPlayer(this);
        CurrentState.Init();
    }
}
