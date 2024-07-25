using Player;
using UnityEngine;

public class PlayerEnvironmentIntersection : MonoBehaviour
{
    // TODO _j optimize, collect colliders check OnTrigger only for them
    public Collider2D floorTileMapColider;
    public Collider2D wallTileMapColider;
    public Collider2D roadTileMapColider;

    public MovePlayer movePlayerComponent;

    private enum EnvironmentKind 
    {
        Unknown,
        Floor,
        Road,
        Wall
    }
    
    void Start()
    {
        Init();
    }

    void Init()
    {
        Debug.Log("_j PlayerEnvironmentIntersection Init()");

        if (!floorTileMapColider) Debug.LogError("No floorTileMapColider given");
        if (!wallTileMapColider) Debug.LogError("No wallTileMapColider given");
        if (!roadTileMapColider) Debug.LogError("No roadTileMapColider given");
    }

    EnvironmentKind SharedTriggerRoutine(Collider2D colider) {
        if (colider.GetInstanceID() == floorTileMapColider.GetInstanceID()) {
            return EnvironmentKind.Floor;
        }
        if (colider.GetInstanceID() == roadTileMapColider.GetInstanceID()) {
            return EnvironmentKind.Road;
        }
        if (colider.GetInstanceID() == wallTileMapColider.GetInstanceID()) {
            return EnvironmentKind.Wall;
        }
        return EnvironmentKind.Unknown;
    }

    void OnTriggerEnter2D(Collider2D colider)
    {
        if (SharedTriggerRoutine(colider) == EnvironmentKind.Floor) {
            Debug.Log($"_j PlayerEnvironmentIntersection OnTriggerEnter2D floor");
            movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() * 1.5f);
        }
        if (SharedTriggerRoutine(colider) == EnvironmentKind.Road) {
            Debug.Log($"_j PlayerEnvironmentIntersection OnTriggerEnter2D road");
            movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() * 2f);
        }
        if (SharedTriggerRoutine(colider) == EnvironmentKind.Wall) {
            Debug.Log($"_j PlayerEnvironmentIntersection OnTriggerEnter2D wall");
            movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() * 0.1f);
        }
    }

    void OnTriggerStay2D(Collider2D colider)
    {
        // GameObject go = colider.gameObject;
        // Debug.Log($"_j PlayerEnvironmentIntersection OnTriggerStay2D go.name: {go.name}");
    }

    void OnTriggerExit2D(Collider2D colider)
    {
        if (SharedTriggerRoutine(colider) == EnvironmentKind.Floor) {
            movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() / 1.5f);
        }
        if (SharedTriggerRoutine(colider) == EnvironmentKind.Road) {
            movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() / 2f);
        }
        if (SharedTriggerRoutine(colider) == EnvironmentKind.Wall) {
            movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() / 0.1f);
        }
    }
}
