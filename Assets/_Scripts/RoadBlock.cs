using UnityEngine;

public class RoadBlock : MonoBehaviour
{
    private Transform _transform;
    public Vector3 Position => _transform.position;

    public bool HasPlatform { get; private set; } = true;

    [SerializeField] private bool _isSpending;
    public bool IsSpending => _isSpending;

    [Header("Neighbors")]
    [SerializeField] private RoadBlock RightNeighbor;
    [SerializeField] private RoadBlock LeftNeighbor;
    [SerializeField] private RoadBlock UpNeighbor;
    [SerializeField] private RoadBlock DownNeighbor;

    private void Awake()
    {
        _transform = GetComponent<Transform>();

        HasPlatform = !_isSpending;

        if (_transform.childCount > 0)
            _transform.GetChild(0)?.gameObject.SetActive(HasPlatform);
    }

    public RoadBlock GetNext(Vector2 direction)
    {
        RoadBlock nextElement = null;

        if (direction == Vector2.right)
            nextElement = RightNeighbor;

        if (direction == Vector2.left)
            nextElement = LeftNeighbor;

        if (direction == Vector2.up)
            nextElement = UpNeighbor;

        if (direction == Vector2.down)
            nextElement = DownNeighbor;

        return nextElement;
    }

    public void GetPlatform(Player player)
    {
        player.Stack.AddPlatform();

        HasPlatform = false;

        if (_transform.childCount > 0)
            _transform.GetChild(0)?.gameObject.SetActive(false);
    }

    public void SetPlatform(Player player)
    {
        player.Stack.RemovePlatform();

        HasPlatform = true;

        if (_transform.childCount > 0)
            _transform.GetChild(0)?.gameObject.SetActive(true);
    } 

    public void SetNeighbors(Neighbors neighbors)
    {
        RightNeighbor = neighbors.RightNeighbor;
        LeftNeighbor = neighbors.LeftNeighbor;
        UpNeighbor = neighbors.UpNeighbor;
        DownNeighbor = neighbors.DownNeighbor;
    }
}
