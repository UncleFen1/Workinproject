using System.Collections.Generic;
using Player;
using UnityEngine;
using Zenject;
using Roulettes;
using UnityEngine.Tilemaps;

public class PlayerEnvironmentIntersection : MonoBehaviour
{
    // TODO _j optimize, collect colliders check OnTrigger only for them
    // TODO _j link with DI
    private Collider2D floorTileMapColider;
    private Collider2D pathTileMapColider;
    private Collider2D wallTileMapColider;

    public MovePlayer movePlayerComponent;
    public PlayerHealth healthPlayerComponent;

    private EnvironmentRoulette environmentRoulette;

    private Dictionary<EnvironmentKind, bool> isOnEnvironmentMap = new Dictionary<EnvironmentKind, bool>
    {
        { EnvironmentKind.Unknown, false },
        { EnvironmentKind.Floor, false },
        { EnvironmentKind.Path, false },
        { EnvironmentKind.Wall, false },
    };

    public float eventInterval = 1f;
    private float lastEventTime = float.MinValue;

    [Inject]
    private void InitBindings(EnvironmentRoulette er) {
        environmentRoulette = er;
    }

    void Start()
    {
        Init();
    }

    void Init()
    {
        var grid = GameObject.FindObjectOfType<Grid>();
        var colliders = grid.GetComponentsInChildren<TilemapCollider2D>();
        
        // TODO _j Andrey (inform) link by names so far... floor path walls... or have to add some scripts there
        for (int i = 0; i < colliders.Length; i++) {
            var collider = colliders[i];
            var colliderNameInvariant = collider.name.ToLowerInvariant();
            if (colliderNameInvariant.Contains("floor")) {
                floorTileMapColider = collider;
            } else if (colliderNameInvariant.Contains("path")) {
                pathTileMapColider = collider;
            } else if (colliderNameInvariant.Contains("wall")) {
                wallTileMapColider = collider;
            } else {
                Debug.LogWarning("unassigned collider: " + collider.name);
            }
        }

        if (!floorTileMapColider) Debug.LogError("No floorTileMapColider given");
        if (!pathTileMapColider) Debug.LogError("No pathTileMapColider given");
        if (!wallTileMapColider) Debug.LogError("No wallTileMapColider given");

        if (!movePlayerComponent) Debug.LogError("No movePlayerComponent given");
        if (!healthPlayerComponent) Debug.LogError("No healthPlayerComponent given");
    }

    EnvironmentKind SharedTriggerRoutine(Collider2D colider)
    {
        if (colider.GetInstanceID() == floorTileMapColider.GetInstanceID())
        {
            return EnvironmentKind.Floor;
        }
        if (colider.GetInstanceID() == pathTileMapColider.GetInstanceID())
        {
            return EnvironmentKind.Path;
        }
        if (colider.GetInstanceID() == wallTileMapColider.GetInstanceID())
        {
            return EnvironmentKind.Wall;
        }
        return EnvironmentKind.Unknown;
    }

    void OnTriggerEnter2D(Collider2D colider)
    {
        if (SharedTriggerRoutine(colider) == EnvironmentKind.Floor)
        {
            Debug.Log($"_j PlayerEnvironmentIntersection OnTriggerEnter2D floor");
            isOnEnvironmentMap[EnvironmentKind.Floor] = true;
            movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() * 1.5f);
        }
        if (SharedTriggerRoutine(colider) == EnvironmentKind.Path)
        {
            Debug.Log($"_j PlayerEnvironmentIntersection OnTriggerEnter2D path");
            isOnEnvironmentMap[EnvironmentKind.Path] = true;
            movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() * 2f);
        }
        if (SharedTriggerRoutine(colider) == EnvironmentKind.Wall)
        {
            Debug.Log($"_j PlayerEnvironmentIntersection OnTriggerEnter2D wall");
            isOnEnvironmentMap[EnvironmentKind.Wall] = true;
            movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() * 0.1f);
        }
    }

    void OnTriggerStay2D(Collider2D colider) {
        // for some reason Stay event stopped propogate if doesn't move
     }

    void OnTriggerExit2D(Collider2D colider)
    {
        if (SharedTriggerRoutine(colider) == EnvironmentKind.Floor)
        {
            isOnEnvironmentMap[EnvironmentKind.Floor] = false;
            movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() / 1.5f);
        }
        if (SharedTriggerRoutine(colider) == EnvironmentKind.Path)
        {
            isOnEnvironmentMap[EnvironmentKind.Path] = false;
            movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() / 2f);
        }
        if (SharedTriggerRoutine(colider) == EnvironmentKind.Wall)
        {
            isOnEnvironmentMap[EnvironmentKind.Wall] = false;
            movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() / 0.1f);
        }
    }

    void FixedUpdate()
    {
        // TODO _j probably timers are needed for every environment and should drop timers on out (care of quick switch envs, left-right-left-right)
        if (Time.time > lastEventTime + eventInterval)
        {
            foreach (EnvironmentKind environmentKind in EnvironmentKind.GetValues(typeof(EnvironmentKind)))
            {
                if (environmentKind == EnvironmentKind.Unknown) continue;

                if (isOnEnvironmentMap[environmentKind])
                {
                    var envKind = environmentRoulette.environmentKindsMap[environmentKind];
                    if (envKind.modifier == EnvironmentModifier.Damage)
                    {
                        healthPlayerComponent.TakePlayerDamage(10);
                        // TODO _j make public UnityAction to assign with Editor?
                    }
                    if (envKind.modifier == EnvironmentModifier.Heal)
                    {
                        healthPlayerComponent.HealPlayer(10);
                    }
                }
            }

            lastEventTime = Time.time;
        }
    }
}
