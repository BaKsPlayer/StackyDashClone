using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenInput : BaseInput, IDragHandler, IBeginDragHandler
{
    private bool isAbleToMove = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        isAbleToMove = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isAbleToMove)
            return;

        Vector2 direction = Vector2.zero;

        if (Mathf.Abs(eventData.delta.x) >= Mathf.Abs(eventData.delta.y))
        {
            if (eventData.delta.x >= 0)
                direction = Vector2.right;
            else
                direction = Vector2.left;
        }
        else
        {
            if (eventData.delta.y >= 0)
                direction = Vector2.up;
            else
                direction = Vector2.down;
        }

        SetDirection(direction);

        isAbleToMove = false;

    }
}
