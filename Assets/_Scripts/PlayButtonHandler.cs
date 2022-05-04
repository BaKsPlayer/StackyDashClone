using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonHandler : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentLevel").ToString() + "LVL");
        GameStats.Instance?.ResetStats();
    }
}
