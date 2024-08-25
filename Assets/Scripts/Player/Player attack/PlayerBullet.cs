using Roulettes;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int damage = 20;

    private PlayerRoulette playerRoulette;
    public void LinkPlayerRoulette(PlayerRoulette er)
    {
        playerRoulette = er;
        ApplyRouletteModifiers();
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
}