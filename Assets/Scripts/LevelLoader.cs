using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadLevelByNumber(int levelNumber)
    {
        // Optional: Add check if scene exists in build
        if (levelNumber >= 0 && levelNumber < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(levelNumber);
        }
        else
        {
            Debug.LogWarning("Level " + levelNumber + " is not in the Build Settings!");
        }
    }
}