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

    public GameObject markerPrefab; // Assign your VisitedMarker prefab in Inspector
    private HashSet<Vector2Int> visitedTiles = new HashSet<Vector2Int>();

    private HashSet<Vector2Int> allTilePositions = new HashSet<Vector2Int>();

    [SerializeField] InputHandler inputHandler;

    private bool allTilesVisited;
    
    private void Start()
    {
        SuccesPanel.SetActive(false);

        allTilesVisited = false;

        GameObject[] allTiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject tile in allTiles)
        {
            Vector2 pos = tile.transform.position;
            Vector2Int gridPos = new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
            allTilePositions.Add(gridPos);
        }

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
        if (visitedTiles.SetEquals(allTilePositions))
        {
            
            allTilesVisited = true;
            // You could also trigger a win panel here, if not using the circle goal
        }

        isMoving = false;

        // Wait one frame to ensure final position update
        yield return null;

        Collider2D hit = Physics2D.OverlapPoint(transform.position, goalLayer);
        bool touchingGoal = (hit != null);
        if (touchingGoal && allTilesVisited)
        {
            SuccesPanel.SetActive(true);
        }
        else
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

            inputHandler.LoadSameScene();


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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))
        {
            
            Vector2 tilePos = collision.transform.position;
            Vector2Int tileGridPos = new Vector2Int(Mathf.RoundToInt(tilePos.x), Mathf.RoundToInt(tilePos.y));

            if (visitedTiles.Contains(tileGridPos))
            {
                
                inputHandler.LoadSameScene();

            }
            else
            {
                visitedTiles.Add(tileGridPos);
                Instantiate(markerPrefab, tilePos, Quaternion.identity);
            }
        }
        else if(collision.CompareTag("BoundaryZone"))
        {
            Debug.Log("Player exited boundary!");
            inputHandler.LoadSameScene();
        }


    }


}