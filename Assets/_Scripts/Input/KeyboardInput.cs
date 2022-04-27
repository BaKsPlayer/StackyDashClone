using UnityEngine;

public class KeyboardInput : BaseInput
{
    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKeyUp(KeyCode.W))
            direction = Vector2.up;
        else
            if (Input.GetKeyUp(KeyCode.A))
            direction = Vector2.left;
        else
            if (Input.GetKeyUp(KeyCode.S))
            direction = Vector2.down;
        else
            if (Input.GetKeyUp(KeyCode.D))
            direction = Vector2.right;
        else
            return;

        SetDirection(direction);
    }
}
