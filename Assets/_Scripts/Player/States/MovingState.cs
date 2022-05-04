using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovingState", menuName = "PlayerStates/MovingState", order = 1)]
public class MovingState : State
{
    private Vector2 _direction;

    private RoadBlock _currentTarget;
    private RoadBlock _lastTarget;

    private int _targetIndex;

    private List<RoadBlock> _path;

    [SerializeField] private State _idleState;

    public override void Init()
    {
        base.Init();

        _direction = _player.MoveDirection;

        _path = new List<RoadBlock>();
        _targetIndex = 0;

        _currentTarget = _player.CurrentBlock;
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
            _player.SetCurrentRoadBlock(_currentTarget);

            if (_currentTarget.IsSpending)
            {
                if (!_currentTarget.HasPlatform)
                    _currentTarget.SetPlatform(_player);
            }
            else
            {
                if (_currentTarget.HasPlatform)
                {
                    _currentTarget.GetPlatform(_player);
                    GameStats.Instance.IncreaseScore();
                }
            }
            
            _targetIndex++;

            if (_targetIndex == _path.Count)
                _player.SetState(_idleState);
            else
                _currentTarget = _path[_targetIndex];

            Debug.Log(_player.Stack.ChildCount);

            if (_currentTarget.IsSpending && !_currentTarget.HasPlatform)
                if (_player.Stack.ChildCount <= 1)
                    _player.Stop();
        }
        else
            _player.Movement.Move(_currentTarget);
    }

}
