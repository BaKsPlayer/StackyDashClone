using UnityEngine;

public class Stack : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private GameObject _platformPrefab;

    [SerializeField] private Transform _character;
    private Vector3 _startCharacterPostion;

    public int ChildCount { get; private set; }

    private float _stackHeight => _platformPrefab.transform.localScale.y * (ChildCount - 1);

    private void Awake()
    {
        _startCharacterPostion = _character.localPosition;
    }

    public void AddPlatform()
    { 
        var newPlatform = Instantiate(_platformPrefab, transform);
        ChildCount++;

        newPlatform.transform.localPosition = new Vector3(0, _stackHeight, 0);

        FitCharacterPosition();
    }

    public void RemovePlatform()
    {
        if (ChildCount <= 1)
            return;
        
        var lastChild = transform.GetChild(transform.childCount - 1).gameObject;
        Destroy(lastChild);
        ChildCount--;

        FitCharacterPosition();
    }

    private void FitCharacterPosition()
    {
        _character.localPosition = new Vector3(_startCharacterPostion.x, _startCharacterPostion.y + _stackHeight, _startCharacterPostion.z);
    }
}
