using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        GameStats.Instance.OnScoreChanged += ScoreChanged;
        ScoreChanged();
    }

    private void OnDisable()
    {
        GameStats.Instance.OnScoreChanged -= ScoreChanged;
    }

    private void ScoreChanged()
    {
        _text.text = GameStats.Instance.Score.ToString();
    }
}
