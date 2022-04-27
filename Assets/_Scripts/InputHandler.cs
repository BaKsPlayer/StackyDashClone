using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private BaseInput _input;

    private void Awake()
    {
        _input = GetComponent<BaseInput>();
    }

    private void OnEnable()
    {
        _input.OnDirectionSettled += DirectionSettled;
    }

    private void OnDisable()
    {
        _input.OnDirectionSettled -= DirectionSettled;
    }

    private void DirectionSettled(Vector2 direction)
    {
        Debug.Log(direction);
    }
}
