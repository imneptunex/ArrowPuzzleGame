using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Vector2Int gridPosition;
    private bool isMoving = false;

    public float moveSpeed = 5f;
    public int gridSize = 4;

    public Collider2D playerCollider;         // your arrow's 2D collider
    public LayerMask goalLayer;               // the circle’s layer

    public Transform startPosition; // 🆕 Drag your StartPosition object here in Inspector

    public GameObject SuccesPanel;

    private void Start()
    {
        SuccesPanel.SetActive(false);

        if (startPosition != null)
        {
            // Move player to the StartPosition GameObject's position
            transform.position = startPosition.position;

            // Set initial grid position to StartPosition's grid coordinates
            gridPosition = new Vector2Int(
                Mathf.RoundToInt(startPosition.position.x),
                Mathf.RoundToInt(startPosition.position.y)
            );
        }
        else
        {
            Debug.LogWarning("StartPosition not assigned! Using current transform position.");
            Vector3 startPos = transform.position;
            gridPosition = new Vector2Int(Mathf.RoundToInt(startPos.x), Mathf.RoundToInt(startPos.y));
        }
    }

    [System.Obsolete]
    public void StartMovement(List<Command> commands)
    {
        if (!isMoving)
        {
            StartCoroutine(ExecuteCommands(new List<Command>(commands)));
        }
    }

    [System.Obsolete]
    private IEnumerator ExecuteCommands(List<Command> commands)
    {
        isMoving = true;

        foreach (var cmd in commands)
        {
            SetRotationForDirection(cmd.direction); // 🆕 Set rotation before executing steps

            for (int i = 0; i < cmd.steps; i++)
            {
                Vector2Int target = gridPosition + cmd.direction;
                gridPosition = target;

                Vector3 worldPos = new Vector3(gridPosition.x, gridPosition.y, 0f);
                yield return MoveTo(worldPos);
            }
        }

        isMoving = false;

        // Wait one frame to ensure final position update
        yield return null;

        Collider2D hit = Physics2D.OverlapPoint(transform.position, goalLayer);
        bool touchingGoal = (hit != null);

        if (!touchingGoal)
        {
            

            // Reset position
            if (startPosition != null)
            {
                transform.position = startPosition.position;
                gridPosition = new Vector2Int(
                    Mathf.RoundToInt(startPosition.position.x),
                    Mathf.RoundToInt(startPosition.position.y)
                );
            }

            // Clear commands
            InputHandler inputHandler = FindObjectOfType<InputHandler>();
            if (inputHandler != null)
            {
                inputHandler.ResetCommands();
            }
        }else
        {
            SuccesPanel.SetActive(true);
        }
    }

        private IEnumerator MoveTo(Vector3 targetPos)
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
    }

    private void SetRotationForDirection(Vector2Int dir)
    {
        float zRotation = 0f;

        if (dir == Vector2Int.right)
            zRotation = 0f;
        else if (dir == Vector2Int.up)
            zRotation = 90f;
        else if (dir == Vector2Int.left)
            zRotation = 180f;
        else if (dir == Vector2Int.down)
            zRotation = 270f;

        transform.rotation = Quaternion.Euler(0f, 0f, zRotation);
    }
}