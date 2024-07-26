using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float attackRange = 1f; // Дальность атаки
    public float attackDamage = 10f; // Урон атаки
    public float attackRate = 1f; // Частота атаки в ударах в секунду
    public float nextAttackTime = 0f; // Время до следующей атаки
    public int meleDamage = 40; // Урон атакой
    public Transform attackPoint; // Точка, откуда будет производиться атака
    public LayerMask enemyLayers; // Слой врагов
    public float attackAngle = 60f; // Угол сектора атаки в градусах
    public Transform attackSector; // Сектор атаки (пустой объект)

    public GameObject attackEffectPrefab; // Префаб 2D объекта, который будет спауниться
    public float effectDuration = 2f; // Время жизни эффекта в секундах

    void Update()
    {
        // Проверяем направление сектора атаки в сторону курсора
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)attackPoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        attackSector.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Проверяем, нажата ли кнопка атаки
        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate; // Обновляем время следующей атаки
        }
    }

    void Attack()
    {
        // Определяем угол атаки
        Vector2 direction = attackSector.up; // Используем направление сектора для проверки угла
        float angleToMouse = Vector2.Angle(attackSector.up, direction);

        // Проверяем, попадает ли курсор в сектор атаки
        if (angleToMouse <= attackAngle / 2)
        {
            // Определяем область атаки
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            // Наносим урон всем врагам в пределах области атаки
            foreach (Collider2D enemy in hitEnemies)
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(meleDamage);
                }
            }

            // Появление 2D объекта в области атаки
            SpawnAttackEffect();
        }
    }

    private void SpawnAttackEffect()
    {
        // Получаем позицию курсора в мировых координатах
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Вычисляем направление к курсору
        Vector2 direction = (mousePosition - (Vector2)attackPoint.position).normalized;

        // Вычисляем позицию для эффекта на расстоянии attackRange в заданном направлении
        Vector2 spawnPosition = (Vector2)attackPoint.position + direction * attackRange;

        // Создаем эффект в вычисленной позиции
        if (attackEffectPrefab != null)
        {
            // Рассчитываем угол поворота для эффекта
            float effectAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Создаем экземпляр эффекта с поворотом
            GameObject effect = Instantiate(attackEffectPrefab, spawnPosition, Quaternion.Euler(0, 0, effectAngle));
            Destroy(effect, effectDuration); // Уничтожаем объект через заданное время
        }
    }

    // Для отображения области атаки в редакторе
    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange); // Визуализируем область атаки

            // Визуализируем угол атаки
            Vector3 leftBoundary = Quaternion.Euler(0, 0, -attackAngle / 2) * attackSector.up * attackRange + attackPoint.position;
            Vector3 rightBoundary = Quaternion.Euler(0, 0, attackAngle / 2) * attackSector.up * attackRange + attackPoint.position;

            Gizmos.DrawLine(attackPoint.position, leftBoundary);
            Gizmos.DrawLine(attackPoint.position, rightBoundary);
        }
    }
}