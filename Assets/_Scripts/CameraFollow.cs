using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        Vector3 playerPosition = new Vector3(_transform.position.x, _transform.position.y, _playerTransform.position.z);

        _transform.position = playerPosition;
    }
}
