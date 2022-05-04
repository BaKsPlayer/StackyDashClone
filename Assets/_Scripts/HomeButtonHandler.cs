using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButtonHandler : MonoBehaviour
{
    public void OpenMainScene()
    {
        SceneManager.LoadScene("Main");
    } 
}
