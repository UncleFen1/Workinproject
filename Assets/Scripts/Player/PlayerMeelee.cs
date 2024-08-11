using System.Collections;
using System.Collections.Generic;
using Roulettes;
using UnityEditor;
using UnityEngine;
using Zenject;

public class MeleeAttack : MonoBehaviour
{
    public float attackRange = 1f;
    public float attackDamage = 10f;
    public float attackRate = 1f;
    public float nextAttackTime = 0f;
    public int meleDamage = 40;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackAngle = 60f;
    public Transform attackSector;

    public GameObject attackEffectPrefab;
    public float effectDuration = 2f;

    private PlayerRoulette playerRoulette;
    [Inject]
    private void InitBindings(PlayerRoulette pr)
    {
        playerRoulette = pr;
        ApplyRouletteModifiers();
    }
    void ApplyRouletteModifiers()
    {
        var mod = playerRoulette.playerKindsMap[PlayerKind.MeleeDamage].modifier;
        switch (mod)
        {
            case PlayerModifier.Unchanged:
                break;
            case PlayerModifier.Increased:
                meleDamage *= 2;
                break;
            case PlayerModifier.Decreased:
                meleDamage /= 2;
                break;
            default:
                Debug.LogWarning("_j unknown modifier");
                break;
        }

        mod = playerRoulette.playerKindsMap[PlayerKind.AttackRate].modifier;
        switch (mod)
        {
            case PlayerModifier.Unchanged:
                break;
            case PlayerModifier.Increased:
                attackRate *= 2;
                break;
            case PlayerModifier.Decreased:
                attackRate /= 2;
                break;
            default:
                Debug.LogWarning("_j unknown modifier");
                break;
        }

        mod = playerRoulette.playerKindsMap[PlayerKind.AttackRange].modifier;
        switch (mod)
        {
            case PlayerModifier.Unchanged:
                break;
            case PlayerModifier.Increased:
                attackRange *= 2;
                break;
            case PlayerModifier.Decreased:
                attackRange /= 2;
                break;
            default:
                Debug.LogWarning("_j unknown modifier");
                break;
        }
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)attackPoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        attackSector.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    void Attack()
    {
        Vector2 direction = attackSector.up;
        float angleToMouse = Vector2.Angle(attackSector.up, direction);

        if (angleToMouse <= attackAngle / 2)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(meleDamage);
                }
            }

            SpawnAttackEffect();
        }
    }

    private void SpawnAttackEffect()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)attackPoint.position).normalized;

        Vector2 spawnPosition = (Vector2)attackPoint.position + direction * attackRange;

        if (attackEffectPrefab != null)
        {
            float effectAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            GameObject effect = Instantiate(attackEffectPrefab, spawnPosition, Quaternion.Euler(0, 0, effectAngle));
            Destroy(effect, effectDuration);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);

            Vector3 leftBoundary = Quaternion.Euler(0, 0, -attackAngle / 2) * attackSector.up * attackRange + attackPoint.position;
            Vector3 rightBoundary = Quaternion.Euler(0, 0, attackAngle / 2) * attackSector.up * attackRange + attackPoint.position;

            Gizmos.DrawLine(attackPoint.position, leftBoundary);
            Gizmos.DrawLine(attackPoint.position, rightBoundary);
        }
    }
}