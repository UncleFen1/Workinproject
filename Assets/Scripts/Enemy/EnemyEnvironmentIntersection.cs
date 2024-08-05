using System.Collections.Generic;
using GameGrid;
using Roulettes;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyEnvironmentIntersection : MonoBehaviour
{
    private Collider2D floorTileMapCollider;
    private Collider2D pathTileMapCollider;
    private Collider2D wallTileMapCollider;

    private EnemyMovement enemyMovement;
    private EnemyHealth enemyHealth;

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

    // [Inject]
    public void LinkEnemyEnvironmentIntersection(EnvironmentRoulette er, GridController gc)
    {
        environmentRoulette = er;

        floorTileMapCollider = gc.floor;
        pathTileMapCollider = gc.path;
        wallTileMapCollider = gc.wall;

        // player dependencies could be taken from this.gameObject.GetComponent since it's important to be on Player GO and to have OnTrigger events
        enemyMovement = this.gameObject.GetComponent<EnemyMovement>();
        enemyHealth = this.gameObject.GetComponent<EnemyHealth>();
    }

    void Start()
    {
        Init();
    }

    void Init()
    {
        if (!floorTileMapCollider) Debug.LogError("No floorTileMapCollider given");
        if (!pathTileMapCollider) Debug.LogError("No pathTileMapCollider given");
        if (!wallTileMapCollider) Debug.LogError("No wallTileMapCollider given");

        if (!enemyMovement) Debug.LogError("No movePlayerComponent given");
        if (!enemyHealth) Debug.LogError("No healthPlayerComponent given");
    }

    EnvironmentKind SharedTriggerRoutine(Collider2D collider)
    {
        // TODO _j could be a Dictionary to iterate on
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
            isOnEnvironmentMap[EnvironmentKind.Floor] = true;
            enemyMovement.speed *= 1.5f;
        }
        if (SharedTriggerRoutine(collider) == EnvironmentKind.Path)
        {
            isOnEnvironmentMap[EnvironmentKind.Path] = true;
            enemyMovement.speed *= 2f;
        }
        if (SharedTriggerRoutine(collider) == EnvironmentKind.Wall)
        {
            isOnEnvironmentMap[EnvironmentKind.Wall] = true;
            enemyMovement.speed *= 0.1f;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (SharedTriggerRoutine(collider) == EnvironmentKind.Floor)
        {
            isOnEnvironmentMap[EnvironmentKind.Floor] = false;
            enemyMovement.speed /= 1.5f;
        }
        if (SharedTriggerRoutine(collider) == EnvironmentKind.Path)
        {
            isOnEnvironmentMap[EnvironmentKind.Path] = false;
            enemyMovement.speed /= 2f;
        }
        if (SharedTriggerRoutine(collider) == EnvironmentKind.Wall)
        {
            isOnEnvironmentMap[EnvironmentKind.Wall] = false;
            enemyMovement.speed /= 0.1f;
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
