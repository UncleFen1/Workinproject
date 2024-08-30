using System.Collections.Generic;
using Player;
using UnityEngine;
using Zenject;
using Roulettes;
using GameGrid;
using UnityEngine.Tilemaps;
using UnityEngine.Rendering;
using System.Linq;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerEnvironmentIntersection : MonoBehaviour
{
    private const int ENVIRONMENT_PLAYER_DAMAGE = 1;
    private const int ENVIRONMENT_PLAYER_HEAL = 1;
    // private const Color can't be, but need for search
    private Color DEFAULT_WALL_COLOR = Color.white;
    private Color TRANSPARENT_WALL_COLOR = new Color(1f, 1f, 1f, 0.35f);

    private List<GridController> gridControllerList;

    private MovePlayer movePlayerComponent;
    private PlayerHealth healthPlayerComponent;
    private SortingGroup sortingGroupComponent;

    private EnvironmentRoulette environmentRoulette;

    private EnvironmentColliderData currentEnvironmentColliderData;
    private EnvironmentColliderData previousEnvironmentColliderData;

    private Dictionary<int, GridController> activeGridControllers = new();

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

    private void ChangeSortingOrder(int sortingOrder)
    {
        sortingGroupComponent.sortingOrder = sortingOrder;
    }

    private void GridStepUp(GridController grid)
    {
        activeGridControllers.Add(grid.GetInstanceID(), grid);
        
        if (activeGridControllers.Count == 1)
        {
            ChangeSortingOrder(grid.sortingOrder);
        }
        ChangeGridWallsColor(grid, grid.sortingOrder, TRANSPARENT_WALL_COLOR);
    }
    
    private void GridLeft(GridController grid)
    {
        activeGridControllers.Remove(grid.GetInstanceID());
        
        if (activeGridControllers.Count == 1)
        {
            ChangeSortingOrder(activeGridControllers.First().Value.sortingOrder);
        }
        ChangeGridWallsColor(grid, grid.sortingOrder, DEFAULT_WALL_COLOR);
    }

    private void EnvironmentColliderDataChanged(EnvironmentColliderData currentData, EnvironmentColliderData previousData)
    {
        // happens quite often: Floor->Path->Wall->Path->...
        // Debug.Log($"_j environmentColliderDataChanged: {curData.collider.name}, prev: {oldData.collider.name}");
    }

    private void ChangeGridWallsColor(GridController gridController, int sortingOrder, Color color)
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
            // TODO _j it's better to store an array of Tilemap components for this colliders
            var tilemapRenderer = col.GetComponent<TilemapRenderer>();
            if (tilemapRenderer.sortingOrder >= sortingOrder)
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
            
            bool isFound = false;
            EnvironmentKind foundEnvironmentKind = EnvironmentKind.Unknown;
            // TODO _j could use a Dictionary (need to create on Start) instead of the List (floorColliders)
            if (!isFound)
            {
                foreach (var col in floorColliders)
                {
                    if (instanceId == col.GetInstanceID())
                    {
                        isFound = true;
                        foundEnvironmentKind = EnvironmentKind.Floor;
                        break;
                    }
                }
            }
            if (!isFound)
            {
                foreach (var col in pathColliders)
                {
                    if (instanceId == col.GetInstanceID())
                    {
                        isFound = true;
                        foundEnvironmentKind = EnvironmentKind.Path;
                        break;
                    }
                }
            }
            if (!isFound)
            {
                foreach (var col in wallColliders)
                {
                    if (instanceId == col.GetInstanceID())
                    {
                        isFound = true;
                        foundEnvironmentKind = EnvironmentKind.Wall;
                        break;
                    }
                }
            }
            if (!isFound)
            {
                foreach (var col in pillarColliders)
                {
                    if (instanceId == col.GetInstanceID())
                    {
                        isFound = true;
                        foundEnvironmentKind = EnvironmentKind.Pillar;
                        break;
                    }
                }
            }

            if (isFound)
            {
                environmentColliderData.environmentKind = foundEnvironmentKind;
                environmentColliderData.gridController = gridController;

                break;
            }
        }
        
        return environmentColliderData;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        var environmentColliderData = DefineEnvironmentColliderData(collider);
        var environmentKind = environmentColliderData.environmentKind;
        if (environmentKind != EnvironmentKind.Unknown)
        {
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

            if (activeGridControllers.Count == 0)
            {
                GridStepUp(environmentColliderData.gridController);
            }
            else
            {
                if (!activeGridControllers.ContainsKey(environmentColliderData.gridController.GetInstanceID()))
                {
                    GridStepUp(environmentColliderData.gridController);
                }
            }

            if (environmentKind == EnvironmentKind.Floor)
            {
                isOnEnvironmentMap[EnvironmentKind.Floor] = true;
                movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() * 1.5f);
            }
            if (environmentKind == EnvironmentKind.Path)
            {
                isOnEnvironmentMap[EnvironmentKind.Path] = true;
                movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() * 2f);
            }
            if (environmentKind == EnvironmentKind.Wall)
            {
                isOnEnvironmentMap[EnvironmentKind.Wall] = true;
                // movePlayerComponent.SetMovementSpeed(movePlayerComponent.GetMovementSpeed() * 0.1f);
            }
            if (environmentKind == EnvironmentKind.Pillar)
            {
                isOnEnvironmentMap[EnvironmentKind.Pillar] = true;
            }
            // Debug.Log($"_j PlayerEnvironmentIntersection OnTriggerEnter2D {environmentKind}");
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
        if (environmentKind != EnvironmentKind.Unknown)
        {
            if (activeGridControllers.Count > 1 && activeGridControllers.ContainsKey(environmentColliderData.gridController.GetInstanceID()))
            {
                GridLeft(environmentColliderData.gridController);
            }

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
                isOnEnvironmentMap[EnvironmentKind.Pillar] = false;
            }
            // Debug.Log($"_j PlayerEnvironmentIntersection OnTriggerExit2D {environmentKind}");
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
                        healthPlayerComponent.TakePlayerDamage(ENVIRONMENT_PLAYER_DAMAGE);
                    }
                    if (envKind.modifier == EnvironmentModifier.Heal)
                    {
                        healthPlayerComponent.HealPlayer(ENVIRONMENT_PLAYER_HEAL);
                    }
                }
            }

            lastEventTime = Time.time;
        }
    }
}
