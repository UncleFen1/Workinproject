using System.Collections.Generic;
using Player;
using UnityEngine;
using EnvironmentUtils;

public class PlayerEnvironmentIntersection : MonoBehaviour
{
    // TODO _j optimize, collect colliders check OnTrigger only for them
    // TODO _j link with DI
    public Collider2D floorTileMapColider;
    public Collider2D wallTileMapColider;
    public Collider2D roadTileMapColider;

    public MovePlayer movePlayerComponent;
    public PlayerHealth healthPlayerComponent;

    public EnvironmentRoulette environmentRoulette;

    private Dictionary<EnvironmentKind, bool> isOnEnvironmentMap = new Dictionary<EnvironmentKind, bool>
    {
        { EnvironmentKind.Unknown, false },
        { EnvironmentKind.Floor, false },
        { EnvironmentKind.Road, false },
        { EnvironmentKind.Wall, false },
    };

    public float eventInterval = 1f;
    private float lastEventTime = float.MinValue;

    void Start()
    {
        Init();
    }

    void Init()
    {
        if (!floorTileMapColider) Debug.LogError("No floorTileMapColider given");
        if (!wallTileMapColider) Debug.LogError("No wallTileMapColider given");
        if (!roadTileMapColider) Debug.LogError("No roadTileMapColider given");

        if (!movePlayerComponent) Debug.LogError("No movePlayerComponent given");
        if (!healthPlayerComponent) Debug.LogError("No healthPlayerComponent given");

        if (!environmentRoulette) Debug.LogError("No environmentRoulette given");
    }

    EnvironmentKind SharedTriggerRoutine(Collider2D colider)
    {
        if (colider.GetInstanceID() == floorTileMapColider.GetInstanceID())
        {
            return EnvironmentKind.Floor;
        }
        if (colider.GetInstanceID() == roadTileMapColider.GetInstanceID())
        {
            return EnvironmentKind.Road;
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
        if (SharedTriggerRoutine(colider) == EnvironmentKind.Road)
        {
            Debug.Log($"_j PlayerEnvironmentIntersection OnTriggerEnter2D road");
            isOnEnvironmentMap[EnvironmentKind.Road] = true;
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
        if (SharedTriggerRoutine(colider) == EnvironmentKind.Road)
        {
            isOnEnvironmentMap[EnvironmentKind.Road] = false;
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
