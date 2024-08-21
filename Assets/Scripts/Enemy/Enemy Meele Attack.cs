using Roulettes;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public float attackRange = 1f;
    public int attackDamage = 20;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    public Transform player;
    public PlayerHealth playerHealth;

    public Animator animator;

    // TODO _j since it's instantiated from Spawner, there should be some other way to bind dependencies (check similar LinkEnemyRoulette() in other classes)
    private EnemyRoulette enemyRoulette;
    public void LinkEnemyRoulette(EnemyRoulette er) {
        enemyRoulette = er;
        ApplyRouletteModifiers();
    }
    void ApplyRouletteModifiers() {
        var mod = enemyRoulette.enemyKindsMap[EnemyKind.MeleeDamage].modifier;
        switch (mod) {
            case EnemyModifier.Unchanged:
                break;
            case EnemyModifier.Increased:
                attackDamage *= 2;
                break;
            case EnemyModifier.Decreased:
                attackDamage /= 2;
                break;
            default:
                Debug.LogWarning("_j unknown modifier");
                break;
        }

        mod = enemyRoulette.enemyKindsMap[EnemyKind.AttackRate].modifier;
        switch (mod) {
            case EnemyModifier.Unchanged:
                break;
            case EnemyModifier.Increased:
                attackRate *= 2;
                break;
            case EnemyModifier.Decreased:
                attackRate /= 2;
                break;
            default:
                Debug.LogWarning("_j unknown modifier");
                break;
        }
    }

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            playerHealth = playerObject.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (player != null && playerHealth != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
            {
                nextAttackTime = Time.time + 1f / attackRate;
                Attack();
                
            }

            if (playerHealth.currentPlayerHealth <= 0)
            {
                player = null;
                playerHealth = null;
                StopAttackAnimation();
            }
            
            else if (distanceToPlayer > attackRange)
            {
                StopAttackAnimation();
            }

        }
    }

    void Attack()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            playerHealth.TakePlayerDamage(attackDamage);

            animator.SetBool("EnemyMelee", true);
        }
        
    }

    void StopAttackAnimation()
    {
        animator.SetBool("EnemyMelee", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}