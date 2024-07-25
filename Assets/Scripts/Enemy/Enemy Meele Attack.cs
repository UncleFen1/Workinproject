using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;


public class EnemyMeleeAttack : MonoBehaviour
{
    public float attackRange = 1f; // Дистанция атаки
    public int attackDamage = 20; // Урон
    public float attackRate = 2f; // Частота атак
    private float nextAttackTime = 0f; // Время следующей доступной атаки

    public Transform player; // Ссылка на игрока
    public PlayerHealth playerHealth; // Ссылка на компонент здоровья игрока

    void Start()
    {
        // Находим игрока в сцене (вы можете использовать другой способ получения ссылки)
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            playerHealth = playerObject.GetComponent<PlayerHealth>(); // Получаем компонент PlayerHealth
        }
    }

    void Update()
    {
        // Проверяем, существует ли игрок и расстояние до него
        if (player != null && playerHealth != null)
        {
            // Проверяем расстояние до игрока
            if (Vector2.Distance(transform.position, player.position) <= attackRange && Time.time >= nextAttackTime)
            {
                nextAttackTime = Time.time + 1f / attackRate; // Устанавливаем следующее время атаки
                Attack();
            }

            // Проверяем, уничтожен ли игрок
            if (playerHealth.currentPlayerHealth <= 0)
            {
                player = null; // Удаляем ссылку на игрока
                playerHealth = null; // Удаляем ссылку на компонент здоровья
            }
        }
    }

    void Attack()
    {
        // Проверяем, если игрок находится в пределах диапазона
        if (player != null && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            // Наносим урон игроку
            playerHealth.TakePlayerDamage(attackDamage); // Предполагаем, что у игрока есть компонент PlayerHealth
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Отображение радиуса атаки в редакторе
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}