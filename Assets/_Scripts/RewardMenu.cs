using UnityEngine;
using UnityEngine.UI;

public class RewardMenu : MonoBehaviour
{
    [SerializeField] private Text _scoreText;

    public void Fill()
    {
        _scoreText.text = GameStats.Instance.Score.ToString();
    }
}
