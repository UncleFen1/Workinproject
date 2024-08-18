using System.Collections.Generic;
using Player;
using UnityEngine;
using Zenject;
using Roulettes;
using GameGrid;
using UnityEngine.Tilemaps;
using UnityEngine.Rendering;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerEnvironmentIntersection : MonoBehaviour
{
    private Color DEFAULT_WALL_COLOR = Color.white;
    private Color TRANSPARENT_WALL_COLOR = new Color(1f, 1f, 1f, 0.35f);

    private List<GridController> gridControllerList;

    private MovePlayer movePlayerComponent;
    private PlayerHealth healthPlayerComponent;
    private SortingGroup sortingGroupComponent;

    private EnvironmentRoulette environmentRoulette;

    private EnvironmentColliderData currentEnvironmentColliderData;
    private EnvironmentColliderData previousEnvironmentColliderData;

    private GridController currentGridController;
    private GridController previousGridController;

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
    private void InitBindings(EnvironmentRoulette er, List<GridController> gcs)
    {
        environmentRoulette = er;
        gridControllerList = gcs;

        // player dependencies could be taken from this.gameObject.GetComponent since it's important to be on Player GO and to have OnTrigger events
        // movePlayerComponent = pc.movePlayer;
        // healthPlayerComponent = pc.playerHealth;
        movePlayerComponent = this.gameObject.GetComponent<MovePlayer>();
        healthPlayerComponent = this.gameObject.GetComponent<PlayerHealth>();
        sortingGroupComponent = this.gameObject.GetComponent<SortingGroup>();
    }

    void Start()
    {
        Init();
    }

    void Init()
    {
        if (!movePlayerComponent) Debug.LogError("No movePlayerComponent given");
        if (!healthPlayerComponent) Debug.LogError("No healthPlayerComponent given");
        if (!sortingGroupComponent) Debug.LogError("No sortingGroupComponent given");
    }

    private void GridChanged(GridController currentGrid, GridController previousGrid)
    {
        // TODO _j can send event onGridChange by EventBus, if needed
        if (previousGrid != null)
        {
            Debug.Log($"_j gridChanged: {currentGrid.name}, prev: {previousGrid.name}");
        }
        else
        {
            Debug.Log($"_j gridChanged: {currentGrid.name}, prev: {null}");
        }

        ChangeGridWallsColor(previousGrid, DEFAULT_WALL_COLOR);
        sortingGroupComponent.sortingOrder = currentGrid.sortingOrder;
        ChangeGridWallsColor(currentGrid, TRANSPARENT_WALL_COLOR);
    }

    private void EnvironmentColliderDataChanged(EnvironmentColliderData currentData, EnvironmentColliderData previousData)
    {
        // happens quite often: Floor->Path->Wall->Path->...
        // Debug.Log($"_j environmentColliderDataChanged: {curData.collider.name}, prev: {oldData.collider.name}");
    }

    private void ChangeGridWallsColor(GridController gridController, Color color)
    {
        if (gridController == null)
        {
            Debug.LogWarning("grid is null, can't change color");
            return;
        }
        var wallColliders = gridController.wallColliders;
        if (wallColliders == null)
        {
            Debug.LogWarning("grid has no wall colliders, can't change color");
            return;
        }
        foreach (var col in wallColliders)
        {
            // may be it's better to store an array of Tilemap components for this colliders
            var tilemapRenderer = col.GetComponent<TilemapRenderer>();
            if (tilemapRenderer.sortingOrder >= sortingGroupComponent.sortingOrder)
            {
                col.GetComponent<Tilemap>().color = color;
            }
        }
    }

    EnvironmentColliderData DefineEnvironmentColliderData(Collider2D collider)
    {
        var environmentColliderData = new EnvironmentColliderData
        {
            collider = collider
        };

        var instanceId = collider.GetInstanceID();
        foreach (var gridController in gridControllerList)
        {
            var floorColliders = gridController.floorColliders;
            var pathColliders = gridController.pathColliders;
            var wallColliders = gridController.wallColliders;
            var pillarColliders = gridController.pillarColliders;
            
            EnvironmentKind foundEnvironmentKind = EnvironmentKind.Unknown;
            // TODO _j could use a Dictionary instead of the List
            foreach (var col in floorColliders)
            {
                if (instanceId == col.GetInstanceID())
                {
                    foundEnvironmentKind = EnvironmentKind.Floor;
                    break;
                }
            }
            foreach (var col in pathColliders)
            {
                if (instanceId == col.GetInstanceID())
                {
                    foundEnvironmentKind = EnvironmentKind.Path;
                    break;
                }
            }
            foreach (var col in wallColliders)
            {
                if (instanceId == col.GetInstanceID())
                {
                    foundEnvironmentKind = EnvironmentKind.Wall;
                    break;
                }
            }
            foreach (var col in pillarColliders)
            {
                if (instanceId == col.GetInstanceID())
                {
                    foundEnvironmentKind = EnvironmentKind.Pillar;
                    break;
                }
            }

            if (foundEnvironmentKind != EnvironmentKind.Unknown)
            {
                environmentColliderData.environmentKind = foundEnvironmentKind;
                environmentColliderData.gridController = gridController;

                if (currentEnvironmentColliderData == null)
                {
                    currentEnvironmentColliderData = environmentColliderData;
                    EnvironmentColliderDataChanged(currentEnvironmentColliderData, previousEnvironmentColliderData);
                }
                else if (previousEnvironmentColliderData != currentEnvironmentColliderData)
                {
                    previousEnvironmentColliderData = currentEnvironmentColliderData;
                    currentEnvironmentColliderData = environmentColliderData;
                    EnvironmentColliderDataChanged(currentEnvironmentColliderData, previousEnvironmentColliderData);
                }

                if (currentGridController == null)
                {
                    currentGridController = environmentColliderData.gridController;
                    GridChanged(currentGridController, previousGridController);
                }
                else
                {
                    // if (currentGridController.GetInstanceID() != environmentColliderData.gridController.GetInstanceID())
                    if (currentGridController != environmentColliderData.gridController)
                    {
                        previousGridController = currentGridController;
                        currentGridController = environmentColliderData.gridController;
                        GridChanged(currentGridController, previousGridController);
                    }
                }

                return environmentColliderData;
            }
        }
        
        return environmentColliderData;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        var environmentColliderData = DefineEnvironmentColliderData(collider);
        var environmentKind = environmentColliderData.environmentKind;
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

    void OnTriggerStay2D(Collider2D collider)
    {
        // for some reason Stay event stopped propogate if doesn't move
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        var environmentColliderData = DefineEnvironmentColliderData(collider);
        var environmentKind = environmentColliderData.environmentKind;
        if (environmentKind == EnvironmentKind.Floor)
        {
            Debug.Log($"_j PlayerEnvironmentIntersection OnTriggerEnter2D floor left");
            isOnEnvironmentMap[EnvironmentKind.Floor] = false;
            movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() / 1.5f);
        }
        if (environmentKind == EnvironmentKind.Path)
        {
            Debug.Log($"_j PlayerEnvironmentIntersection OnTriggerEnter2D path left");
            isOnEnvironmentMap[EnvironmentKind.Path] = false;
            movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() / 2f);
        }
        if (environmentKind == EnvironmentKind.Wall)
        {
            Debug.Log($"_j PlayerEnvironmentIntersection OnTriggerEnter2D wall left");
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
