using System.Collections.Generic;
using GameEnvironmentIntersection;
using GameGrid;
using Roulettes;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int damage = 20;

    private PlayerRoulette playerRoulette;
    private List<GridController> gridControllerList;
    public void LinkPlayerRoulette(PlayerRoulette er, List<GridController> gcs)
    {
        playerRoulette = er;
        ApplyRouletteModifiers();

        gridControllerList = gcs;
    }
    void ApplyRouletteModifiers()
    {
        var mod = playerRoulette.playerKindsMap[PlayerKind.RangeDamage].modifier;
        switch (mod)
        {
            case PlayerModifier.Unchanged:
                break;
            case PlayerModifier.Increased:
                damage *= 2;
                break;
            case PlayerModifier.Decreased:
                damage /= 2;
                break;
            default:
                Debug.LogWarning("_j unknown modifier");
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
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
