using UnityEngine;

[CreateAssetMenu(fileName = "IdleState", menuName = "PlayerStates/IdleState", order = 0)]
public class IdleState : State
{
    [SerializeField] private State _movingState;

    public override void Init()
    {
        _player.Input.OnDirectionSettled += DirectionSettled;
    }

    private void DirectionSettled(Vector2 direction)
    {
        _player.SetDirection(direction);
        _player.SetState(_movingState);

        _player.Input.OnDirectionSettled -= DirectionSettled;
    }
}
