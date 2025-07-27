using UnityEngine;

public class Command
{
    public Vector2Int direction;
    public int steps;

    public Command(Vector2Int dir, int s)
    {
        direction = dir;
        steps = s;
    }
}