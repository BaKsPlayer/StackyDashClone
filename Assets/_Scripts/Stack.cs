using UnityEngine;

public class Stack : MonoBehaviour
{
    [SerializeField] private GameObject _platformPrefab;

    [SerializeField] private Transform _character;
    private Vector3 _startCharacterPostion;

    private int childCount;

    private float _stackHeight => _platformPrefab.transform.localScale.y * childCount;

    private void Awake()
    {
        _startCharacterPostion = _character.localPosition;
    }

    public void AddPlatform()
    { 
        var newPlatform = Instantiate(_platformPrefab, transform);
        childCount++;

        newPlatform.transform.localPosition = new Vector3(0, _stackHeight, 0);

        FitCharacterPosition();
    }

    public void RemovePlatform()
    {
        if (childCount <= 1)
            return;

        var lastChild = transform.GetChild(transform.childCount - 1).gameObject;
        Destroy(lastChild);
        childCount--;

        FitCharacterPosition();
    }

    private void FitCharacterPosition()
    {
        _character.localPosition = new Vector3(_startCharacterPostion.x, _startCharacterPostion.y + _stackHeight, _startCharacterPostion.z);
    }
}
