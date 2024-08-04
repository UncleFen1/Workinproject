using UnityEngine;
using EnemiesUtils;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    private EnemyRoulette enemyRoulette;
    public void LinkEnemyRoulette(EnemyRoulette er) {
        enemyRoulette = er;
        ApplyRouletteModifiers();
    }
    void ApplyRouletteModifiers() {
        var mod = enemyRoulette.enemyKindsMap[EnemyKind.Health].modifier;
        switch (mod) {
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

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // ����� ����� �������� ������ ������, �������� ��� ����
        Debug.Log("Enemy died!");
        Destroy(gameObject); // ���������� ������ �����
    }
}