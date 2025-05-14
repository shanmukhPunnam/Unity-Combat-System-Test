using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }

    public void Start()
    {
        Instance = this;
    }

    private void LoadScene(string sceneName)
    {
        // Load the scene asynchronously
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void Restart()
    {
        // Restart the current scene
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        LoadScene(currentSceneName);
    }
}

