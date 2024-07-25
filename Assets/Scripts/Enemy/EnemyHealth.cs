using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Максимальное здоровье
    public int currentHealth;   // Текущие очки здоровья

    void Start()
    {
        currentHealth = maxHealth; // Устанавливаем текущее здоровье на максимальное значение в начале
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Уменьшаем текущее здоровье на величину урона
        Debug.Log("Enemy took damage: " + damage + " Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(); // Если здоровье 0 или меньше, вызываем метод смерти
        }
    }

    void Die()
    {
        // Здесь можно добавить эффект смерти, анимацию или звук
        Debug.Log("Enemy died!");
        Destroy(gameObject); // Уничтожаем объект врага
    }
}