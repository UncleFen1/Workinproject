using System.Collections.Generic;
using GameEnvironmentIntersection;
using GameGrid;
using Roulettes;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 20;

    private EnemyRoulette enemyRoulette;
    private List<GridController> gridControllerList;
    public void LinkEnemyRoulette(EnemyRoulette er, List<GridController> gcs)
    {
        enemyRoulette = er;
        ApplyRouletteModifiers();

        gridControllerList = gcs;
    }
    void ApplyRouletteModifiers()
    {
        var mod = enemyRoulette.enemyKindsMap[EnemyKind.RangeDamage].modifier;
        switch (mod)
        {
            case EnemyModifier.Unchanged:
                break;
            case EnemyModifier.Increased:
                damage *= 2;
                break;
            case EnemyModifier.Decreased:
                damage /= 2;
                break;
            default:
                Debug.LogWarning("_j unknown modifier");
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth PlayerHealth = other.GetComponent<PlayerHealth>();
            if (PlayerHealth != null)
            {
                PlayerHealth.TakePlayerDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        var collider = other.collider;
        if (SharedEnvironmentIntersection.CheckColliderHitWallOrPillar(collider, gridControllerList))
        {
            Destroy(gameObject);
        }
    }
}
