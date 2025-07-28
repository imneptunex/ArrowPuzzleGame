using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    public List<Command> commandList = new List<Command>();

    private Vector2Int? currentDirection = null;
    public TextMeshProUGUI commandDisplayText;
    private int currentSteps = 1; // default to 1

   
    public void AddDirection(string dir)
    {
        dir = dir.Trim().ToLower();
        switch (dir)
        {
            case "up": currentDirection = Vector2Int.up; break;
            case "down": currentDirection = Vector2Int.down; break;
            case "left": currentDirection = Vector2Int.left; break;
            case "right": currentDirection = Vector2Int.right; break;
            default: Debug.LogWarning("Unknown direction: \"" + dir + "\""); return;
        }

        
        TryAddCommand();
    }

    public void AddStep(string stepStr)
    {
        if (int.TryParse(stepStr, out int step))
        {
            currentSteps = step;
            
            TryAddCommand();
        }
        else
        {
            
        }
    }

    private void TryAddCommand()
    {
        if (currentDirection.HasValue && currentSteps > 0)
        {
            commandList.Add(new Command(currentDirection.Value, currentSteps));
            
            currentDirection = null;
            currentSteps = 1;
            UpdateCommandDisplay();
        }
    }

    [System.Obsolete]
    public void RunCommands()
    {
        
        FindObjectOfType<PlayerMover>().StartMovement(new List<Command>(commandList));
    }

    public void ResetCommands()
    {
        
        commandList.Clear();
        currentDirection = null;
        currentSteps = 1;
        UpdateCommandDisplay(); // If you’re updating on-screen UI

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    private void UpdateCommandDisplay()
    {
        if (commandDisplayText == null) return;

        if (commandList.Count == 0)
        {

            return;
        }

        string display = "";
        for (int i = 0; i < commandList.Count; i++)
        {
            var cmd = commandList[i];
            string arrow = DirectionToArrow(cmd.direction);
            display += $"{cmd.steps}{arrow}   ";
        }

        commandDisplayText.text = display;
    }


    private string DirectionToArrow(Vector2Int dir)
    {
        string sizeTagStart = "<size=150%>";
        string sizeTagEnd = "</size>";

        if (dir == Vector2Int.right) return sizeTagStart + "→" + sizeTagEnd;
        if (dir == Vector2Int.left) return sizeTagStart + "←" + sizeTagEnd;
        if (dir == Vector2Int.up) return sizeTagStart + "↑  " + sizeTagEnd;
        if (dir == Vector2Int.down) return sizeTagStart + "↓  " + sizeTagEnd;

        return "?";
    }

    public void LoadSameScene()
    {
        // Reload the current active scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void LoadNextScene()
    {
        // Load the next scene in build order
        Scene currentScene = SceneManager.GetActiveScene();
        int nextSceneIndex = currentScene.buildIndex + 1;

        

        SceneManager.LoadScene(nextSceneIndex);
    }

    public void ReturnHome()
    {
        SceneManager.LoadScene(0);
    }

}