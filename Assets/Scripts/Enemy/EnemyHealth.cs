using GameEventBus;
using Roulettes;
using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Animator animator;
    private bool isTakingDamage = false;
    private bool isDead = false;

    public SpriteRenderer spriteRenderer;
    public Sprite deathSprite;

    private EnemyRoulette enemyRoulette;
    private EventBus eventBus;
    public void LinkEnemyRoulette(EnemyRoulette er, EventBus eb)
    {
        enemyRoulette = er;
        eventBus = eb;
        ApplyRouletteModifiers();
    }
    void ApplyRouletteModifiers()
    {
        var mod = enemyRoulette.enemyKindsMap[EnemyKind.Health].modifier;
        switch (mod)
        {
            case EnemyModifier.Unchanged:
                break;
            case EnemyModifier.Increased:
                maxHealth *= 2;
                break;
            case EnemyModifier.Decreased:
                maxHealth /= 2;
                break;
            default:
                Debug.LogWarning("_j unknown modifier");
                break;
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy took damage: " + damage + " Current health: " + currentHealth);
        isTakingDamage = true;

        if (!isDead && currentHealth <= 0)
        {
            isDead = true;
            Die();
        }
    }

    public void Update()
    {
        if (isTakingDamage)
        {
            animator.SetBool("EnemyDamage", true);
            isTakingDamage = false;
        }

        else
        {
            animator.SetBool("EnemyDamage", false);
        }
    }

    public void HealEnemy(int value)
    {
        currentHealth += value;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        Debug.Log("Enemy healed: " + value + " Current health: " + currentHealth);
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1f);

        spriteRenderer.sprite = deathSprite;


        if (TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = true;
        }
        this.enabled = false;
        animator.enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<EdgeCollider2D>().enabled = false;
        GetComponent<EnemyEnvironmentIntersection>().enabled = false;
        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<EnemyShooting>().enabled = false;
        GetComponent<EnemyMeleeAttack>().enabled = false;
        
    }

    void Die()
    {
        animator.SetBool("EnemyDeath", true);
        Debug.Log("Enemy died!");
        eventBus.Raise(new EnemyDieEvent());
        StartCoroutine(DeathCoroutine());
    }
}