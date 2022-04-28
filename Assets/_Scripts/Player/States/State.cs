using UnityEngine;

public abstract class State : ScriptableObject
{
    protected Player _player;

    public virtual void Init() { }

    public void SetPlayer(Player player) => _player = player;

    public virtual void Update() { }
}
