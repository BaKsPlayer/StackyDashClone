using UnityEngine;
using UnityEngine.Events;

public class GameStats
{
    private static GameStats _instance;

    public static GameStats Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameStats();

            return _instance;
        }
    }

    public UnityAction OnScoreChanged;

    public int Score { get; private set; }

    public void IncreaseScore()
    {
        Score++;
        OnScoreChanged?.Invoke();
    }

    public void ResetStats()
    {
        Score = 0;
    }
}
