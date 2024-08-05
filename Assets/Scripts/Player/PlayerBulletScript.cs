using EnemiesUtils;
using UnityEngine;

// TODO _j Andrey, see TODO in EnemyBullet.cs
public class Bullet : MonoBehaviour
{
    public int damage = 20;

    private EnemyRoulette enemyRoulette;
    public void LinkEnemyRoulette(EnemyRoulette er)
    {
        enemyRoulette = er;
        ApplyRouletteModifiers();
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
}