using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void RestartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
