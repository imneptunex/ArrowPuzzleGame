using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private const string LastSceneKey = "LastPlayedScene";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep GameManager across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveCurrentScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt(LastSceneKey, currentScene);
        PlayerPrefs.Save();
    }

    public void LoadLastSceneOrDefault()
    {
        if (PlayerPrefs.HasKey(LastSceneKey))
        {
            int lastScene = PlayerPrefs.GetInt(LastSceneKey);
            SceneManager.LoadScene(lastScene);
        }
        else
        {
            SceneManager.LoadScene(1); // Or 1, if your first playable scene is index 1
        }
    }

    public void ResetSavedScene()
    {
        PlayerPrefs.DeleteKey(LastSceneKey);
    }
}