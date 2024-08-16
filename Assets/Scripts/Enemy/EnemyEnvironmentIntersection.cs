using System.Collections.Generic;
using GameGrid;
using Roulettes;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyEnvironmentIntersection : MonoBehaviour
{
    private List<GridController> gridControllerList;

    private EnemyMovement enemyMovement;
    private EnemyHealth enemyHealth;
    private SpriteRenderer spriteRendererComponent;

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

    private void GridChanged(GridController currentGrid, GridController previousGrid)
    {
        // if (previousGrid != null)
        // {
        //     Debug.Log($"_j enemyGridChanged: {currentGrid.name}, prev: {previousGrid.name}");
        // }
        // else
        // {
        //     Debug.Log($"_j enemyGridChanged: {currentGrid.name}, prev: {null}");
        // }

        spriteRendererComponent.sortingOrder = currentGrid.sortingOrder;
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

            EnvironmentKind foundEnvironmentKind = EnvironmentKind.Unknown;
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
            isOnEnvironmentMap[EnvironmentKind.Floor] = true;
            enemyMovement.speed *= 1.5f;
        }
        if (environmentKind == EnvironmentKind.Path)
        {
            isOnEnvironmentMap[EnvironmentKind.Path] = true;
            enemyMovement.speed *= 2f;
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

    void OnTriggerExit2D(Collider2D collider)
    {
        var environmentColliderData = DefineEnvironmentColliderData(collider);
        var environmentKind = environmentColliderData.environmentKind;
        if (environmentKind == EnvironmentKind.Floor)
        {
            isOnEnvironmentMap[EnvironmentKind.Floor] = false;
            enemyMovement.speed /= 1.5f;
        }
        if (environmentKind == EnvironmentKind.Path)
        {
            isOnEnvironmentMap[EnvironmentKind.Path] = false;
            enemyMovement.speed /= 2f;
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
                        enemyHealth.TakeDamage(10);
                    }
                    if (envKind.modifier == EnvironmentModifier.Heal)
                    {
                        enemyHealth.HealEnemy(10);
                    }
                }
            }

            lastEventTime = Time.time;
        }
    }
}
