using System.Collections.Generic;
using GameGrid;
using Roulettes;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyEnvironmentIntersection : MonoBehaviour
{
    private const int ENVIRONMENT_ENEMY_DAMAGE = 1;
    private const int ENVIRONMENT_ENEMY_HEAL = 1;
    
    private List<GridController> gridControllerList;

    private EnemyMovement enemyMovement;
    private EnemyHealth enemyHealth;
    private SpriteRenderer spriteRendererComponent;

    private EnvironmentRoulette environmentRoulette;

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

    private EnvironmentColliderData currentEnvironmentColliderData;
    private EnvironmentColliderData previousEnvironmentColliderData;

    private GridController currentGridController;
    private GridController previousGridController;

    // [Inject]
    public void LinkEnemyEnvironmentIntersection(EnvironmentRoulette er, List<GridController> gcs)
    {
        environmentRoulette = er;
        gridControllerList = gcs;

        // enemy dependencies could be taken from this.gameObject.GetComponent since it's important to be on Enemy GO and to have OnTrigger events
        enemyMovement = this.gameObject.GetComponent<EnemyMovement>();
        enemyHealth = this.gameObject.GetComponent<EnemyHealth>();
        spriteRendererComponent = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        Init();
    }

    void Init()
    {
        if (!enemyMovement) Debug.LogError("No moveEnemyComponent given");
        if (!enemyHealth) Debug.LogError("No healthEnemyComponent given");
        if (!spriteRendererComponent) Debug.LogError("No spriteRendererComponent given");
    }

    private void ChangeSortingOrder(int sortingOrder)
    {
        spriteRendererComponent.sortingOrder = sortingOrder;
    }

    private void GridStepUp(GridController grid)
    {
        Debug.Log($"_j enemy {this.gameObject.name} GridStepUp Add: {grid.name}");
        ChangeSortingOrder(grid.sortingOrder);
        activeGridControllers.Add(grid.GetInstanceID(), grid);
    }
    
    private void GridLeft(GridController grid)
    {
        Debug.Log($"_j enemy {this.gameObject.name} GridLeft Remove: {grid.name}");
        activeGridControllers.Remove(grid.GetInstanceID());
        foreach (var kv in activeGridControllers)
        {
            // TODO _j it's definitely incorrect
            ChangeSortingOrder(kv.Value.sortingOrder);
            break;
        }
    }

    private void EnvironmentColliderDataChanged(EnvironmentColliderData currentData, EnvironmentColliderData previousData)
    {
        // happens quite often: Floor->Path->Wall->Path->...
        // Debug.Log($"_j environmentColliderDataChanged: {curData.collider.name}, prev: {oldData.collider.name}");
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
                enemyMovement.moveSpeed *= 1.5f;
            }
            if (environmentKind == EnvironmentKind.Path)
            {
                isOnEnvironmentMap[EnvironmentKind.Path] = true;
                enemyMovement.moveSpeed *= 2f;
            }
            if (environmentKind == EnvironmentKind.Wall)
            {
                isOnEnvironmentMap[EnvironmentKind.Wall] = true;
                // enemyMovement.speed *= 0.1f;
            }
            if (environmentKind == EnvironmentKind.Pillar)
            {
                // Debug.Log($"_j EnemyEnvironmentIntersection OnTriggerEnter2D pillar");
            }
        }
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
                enemyMovement.moveSpeed /= 1.5f;
            }
            if (environmentKind == EnvironmentKind.Path)
            {
                isOnEnvironmentMap[EnvironmentKind.Path] = false;
                enemyMovement.moveSpeed /= 2f;
            }
            if (environmentKind == EnvironmentKind.Wall)
            {
                isOnEnvironmentMap[EnvironmentKind.Wall] = false;
                // enemyMovement.speed /= 0.1f;
            }
            if (environmentKind == EnvironmentKind.Pillar)
            {
                // Debug.Log($"_j EnemyEnvironmentIntersection OnTriggerEnter2D pillar left");
            }
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
                        enemyHealth.TakeDamage(ENVIRONMENT_ENEMY_DAMAGE);
                    }
                    if (envKind.modifier == EnvironmentModifier.Heal)
                    {
                        enemyHealth.HealEnemy(ENVIRONMENT_ENEMY_HEAL);
                    }
                }
            }

            lastEventTime = Time.time;
        }
    }
}
