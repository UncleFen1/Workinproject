using System.Collections.Generic;
using Player;
using UnityEngine;
using Zenject;
using Roulettes;
using GameGrid;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerEnvironmentIntersection : MonoBehaviour
{
    private List<Collider2D> floorColliders = new List<Collider2D>();
    private List<Collider2D> pathColliders = new List<Collider2D>();
    private List<Collider2D> wallColliders = new List<Collider2D>();
    private List<Collider2D> pillarColliders = new List<Collider2D>();

    private MovePlayer movePlayerComponent;
    private PlayerHealth healthPlayerComponent;

    private EnvironmentRoulette environmentRoulette;

    private Dictionary<EnvironmentKind, bool> isOnEnvironmentMap = new Dictionary<EnvironmentKind, bool>
    {
        { EnvironmentKind.Unknown, false },
        { EnvironmentKind.Floor, false },
        { EnvironmentKind.Path, false },
        { EnvironmentKind.Wall, false },
        { EnvironmentKind.Pillar, false },
    };

    public float eventInterval = 1f;
    private float lastEventTime = float.MinValue;

    [Inject]
    // private void InitBindings(EnvironmentRoulette er, GridController gc, PlayerController pc) {
    private void InitBindings(EnvironmentRoulette er, GridController gc) {
        environmentRoulette = er;

        floorColliders = gc.floorColliders;
        pathColliders = gc.pathColliders;
        wallColliders = gc.wallColliders;
        pillarColliders = gc.pillarColliders;

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

        if (floorColliders.Count == 0) Debug.LogWarning("No floorTileMapCollider given");
        if (pathColliders.Count == 0) Debug.LogWarning("No pathTileMapCollider given");
        if (wallColliders.Count == 0) Debug.LogWarning("No wallTileMapCollider given");
        if (pillarColliders.Count == 0) Debug.LogWarning("No pillarColliders given");

        if (!movePlayerComponent) Debug.LogError("No movePlayerComponent given");
        if (!healthPlayerComponent) Debug.LogError("No healthPlayerComponent given");
    }

    EnvironmentKind DefineEnvironmentKind(Collider2D collider)
    {
        var instanceId = collider.GetInstanceID();
        foreach (var col in floorColliders) {
            if (instanceId == col.GetInstanceID()) {
                return EnvironmentKind.Floor;
            }
        }
        foreach (var col in pathColliders) {
            if (instanceId == col.GetInstanceID()) {
                return EnvironmentKind.Path;
            }
        }
        foreach (var col in wallColliders) {
            if (instanceId == col.GetInstanceID()) {
                return EnvironmentKind.Wall;
            }
        }
        foreach (var col in pillarColliders) {
            if (instanceId == col.GetInstanceID()) {
                return EnvironmentKind.Pillar;
            }
        }
        return EnvironmentKind.Unknown;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        var environmentKind = DefineEnvironmentKind(collider);
        if (environmentKind == EnvironmentKind.Floor)
        {
            Debug.Log($"_j PlayerEnvironmentIntersection OnTriggerEnter2D floor");
            isOnEnvironmentMap[EnvironmentKind.Floor] = true;
            movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() * 1.5f);
        }
        if (environmentKind == EnvironmentKind.Path)
        {
            Debug.Log($"_j PlayerEnvironmentIntersection OnTriggerEnter2D path");
            isOnEnvironmentMap[EnvironmentKind.Path] = true;
            movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() * 2f);
        }
        if (environmentKind == EnvironmentKind.Wall)
        {
            Debug.Log($"_j PlayerEnvironmentIntersection OnTriggerEnter2D wall");
            isOnEnvironmentMap[EnvironmentKind.Wall] = true;
            // movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() * 0.1f);
        }
        if (environmentKind == EnvironmentKind.Pillar)
        {
            Debug.Log($"_j PlayerEnvironmentIntersection OnTriggerEnter2D pillar");
        }
    }

    void OnTriggerStay2D(Collider2D collider) {
        // for some reason Stay event stopped propogate if doesn't move
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        var environmentKind = DefineEnvironmentKind(collider);
        if (environmentKind == EnvironmentKind.Floor)
        {
            isOnEnvironmentMap[EnvironmentKind.Floor] = false;
            movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() / 1.5f);
        }
        if (environmentKind == EnvironmentKind.Path)
        {
            isOnEnvironmentMap[EnvironmentKind.Path] = false;
            movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() / 2f);
        }
        if (environmentKind == EnvironmentKind.Wall)
        {
            isOnEnvironmentMap[EnvironmentKind.Wall] = false;
            // movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() / 0.1f);
        }
        if (environmentKind == EnvironmentKind.Pillar)
        {
            Debug.Log($"_j PlayerEnvironmentIntersection OnTriggerEnter2D pillar left");
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
