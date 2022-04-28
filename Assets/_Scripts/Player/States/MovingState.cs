using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovingState", menuName = "PlayerStates/MovingState", order = 1)]
public class MovingState : State
{
    private Vector2 _direction;

    private RoadElement _currentTarget;
    private RoadElement _lastTarget;

    private int _targetIndex;

    private List<RoadElement> _path;

    [SerializeField] private State _idleState;

    public override void Init()
    {
        base.Init();

        _direction = _player.MoveDirection;

        _path = new List<RoadElement>();
        _targetIndex = 0;

        _currentTarget = _player.CurrentRoadElement.GetNext(_direction);
        _path.Add(_currentTarget);

        _lastTarget = _currentTarget;

        if (_currentTarget == null)
        {
            _player.SetState(_idleState);
            return;
        }

        while (_lastTarget.GetNext(_direction) != null)
        {
            _lastTarget = _lastTarget.GetNext(_direction);
            _path.Add(_lastTarget);
        }
    }

    public override void Update()
    {
        if (_player.transform.position == _currentTarget.Position)
        {
            if (_currentTarget.HasPlatform)
                _currentTarget.GetPlatform(_player);

            _targetIndex++;

            if (_targetIndex == _path.Count)
                _player.SetState(_idleState);
            else
                _currentTarget = _path[_targetIndex];
        }
        else
            _player.Movement.Move(_currentTarget);
    }
}
