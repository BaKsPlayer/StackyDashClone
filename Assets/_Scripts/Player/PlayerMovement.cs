using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   private Transform _transform;
    [SerializeField] private float _speed = 10;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    public void Move(RoadBlock target)
    {
        _transform.position = Vector3.MoveTowards(_transform.position, target.Position, _speed * Time.deltaTime);
    }
}
