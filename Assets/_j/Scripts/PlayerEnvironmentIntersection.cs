using System.Collections.Generic;
using Player;
using UnityEngine;
using Zenject;
using Roulettes;
using GameGrid;
using GamePlayer;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerEnvironmentIntersection : MonoBehaviour
{
    private Collider2D floorTileMapCollider;
    private Collider2D pathTileMapCollider;
    private Collider2D wallTileMapCollider;

    private MovePlayer movePlayerComponent;
    private PlayerHealth healthPlayerComponent;

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
    // private void InitBindings(EnvironmentRoulette er, GridController gc, PlayerController pc) {
    private void InitBindings(EnvironmentRoulette er, GridController gc) {
        environmentRoulette = er;

        floorTileMapCollider = gc.floor;
        pathTileMapCollider = gc.path;
        wallTileMapCollider = gc.wall;

        // player dependencies could be taken from this.gameObject.GetComponent since it's important to be on Player GO and to have OnTrigger events
        // movePlayerComponent = pc.movePlayer;
        // healthPlayerComponent = pc.playerHealth;
        movePlayerComponent = this.gameObject.GetComponent<MovePlayer>();
        healthPlayerComponent = this.gameObject.GetComponent<PlayerHealth>();
    }

    void Start()
    {
        Init();
    }

    void Init()
    {
        // Alternative linking
        // var grid = GameObject.FindObjectOfType<Grid>();
        // var colliders = grid.GetComponentsInChildren<TilemapCollider2D>();
        // for (int i = 0; i < colliders.Length; i++) {
        //     var collider = colliders[i];
        //     var colliderNameInvariant = collider.name.ToLowerInvariant();
        //     if (colliderNameInvariant.Contains("floor")) {
        //         floorTileMapCollider = collider;
        //     } else if (colliderNameInvariant.Contains("path")) {
        //         pathTileMapCollider = collider;
        //     } else if (colliderNameInvariant.Contains("wall")) {
        //         wallTileMapCollider = collider;
        //     } else {
        //         Debug.LogWarning("unassigned collider: " + collider.name);
        //     }
        // }

        if (!floorTileMapCollider) Debug.LogError("No floorTileMapCollider given");
        if (!pathTileMapCollider) Debug.LogError("No pathTileMapCollider given");
        if (!wallTileMapCollider) Debug.LogError("No wallTileMapCollider given");

        if (!movePlayerComponent) Debug.LogError("No movePlayerComponent given");
        if (!healthPlayerComponent) Debug.LogError("No healthPlayerComponent given");
    }

    EnvironmentKind SharedTriggerRoutine(Collider2D collider)
    {
        if (collider.GetInstanceID() == floorTileMapCollider.GetInstanceID())
        {
            return EnvironmentKind.Floor;
        }
        if (collider.GetInstanceID() == pathTileMapCollider.GetInstanceID())
        {
            return EnvironmentKind.Path;
        }
        if (collider.GetInstanceID() == wallTileMapCollider.GetInstanceID())
        {
            return EnvironmentKind.Wall;
        }
        return EnvironmentKind.Unknown;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (SharedTriggerRoutine(collider) == EnvironmentKind.Floor)
        {
            Debug.Log($"_j PlayerEnvironmentIntersection OnTriggerEnter2D floor");
            isOnEnvironmentMap[EnvironmentKind.Floor] = true;
            movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() * 1.5f);
        }
        if (SharedTriggerRoutine(collider) == EnvironmentKind.Path)
        {
            Debug.Log($"_j PlayerEnvironmentIntersection OnTriggerEnter2D path");
            isOnEnvironmentMap[EnvironmentKind.Path] = true;
            movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() * 2f);
        }
        if (SharedTriggerRoutine(collider) == EnvironmentKind.Wall)
        {
            Debug.Log($"_j PlayerEnvironmentIntersection OnTriggerEnter2D wall");
            isOnEnvironmentMap[EnvironmentKind.Wall] = true;
            movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() * 0.1f);
        }
    }

    void OnTriggerStay2D(Collider2D collider) {
        // for some reason Stay event stopped propogate if doesn't move
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (SharedTriggerRoutine(collider) == EnvironmentKind.Floor)
        {
            isOnEnvironmentMap[EnvironmentKind.Floor] = false;
            movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() / 1.5f);
        }
        if (SharedTriggerRoutine(collider) == EnvironmentKind.Path)
        {
            isOnEnvironmentMap[EnvironmentKind.Path] = false;
            movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() / 2f);
        }
        if (SharedTriggerRoutine(collider) == EnvironmentKind.Wall)
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
