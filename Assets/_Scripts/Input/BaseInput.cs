using UnityEngine;
using UnityEngine.Events;

public abstract class BaseInput : MonoBehaviour
{
    public UnityAction<Vector2> OnDirectionSettled;

    public void SetDirection(Vector2 direction)
    {
        OnDirectionSettled?.Invoke(direction);
    }
}
