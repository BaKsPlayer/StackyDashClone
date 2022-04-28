using UnityEngine;

public class RoadElement : MonoBehaviour
{
    private Transform _transform;
    public Vector3 Position => _transform.position;

    public bool HasPlatform { get; private set; } = true;

    [Header("Neighbors")]
    [SerializeField] private RoadElement RightNeighbor;
    [SerializeField] private RoadElement LeftNeighbor;
    [SerializeField] private RoadElement UpNeighbor;
    [SerializeField] private RoadElement DownNeighbor;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    public RoadElement GetNext(Vector2 direction)
    {
        RoadElement nextElement = null;

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
        HasPlatform = false;
        transform.GetChild(0).gameObject.SetActive(false);

        player.Stack.AddPlatform();
    }
}
