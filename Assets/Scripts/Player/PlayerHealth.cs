using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxPlayerHealth = 100; // Максимальное здоровье
    public int currentPlayerHealth;   // Текущие очки здоровья

    void Start()
    {
        currentPlayerHealth = maxPlayerHealth; // Устанавливаем текущее здоровье на максимальное значение в начале
    }

    public void TakePlayerDamage(int damage)
    {
        currentPlayerHealth -= damage; // Уменьшаем текущее здоровье на величину урона
        Debug.Log("Player took damage: " + damage + " Current health: " + currentPlayerHealth);

        if (currentPlayerHealth <= 0)
        {
            PlayerDie(); // Если здоровье 0 или меньше, вызываем метод смерти
        }
    }

    void PlayerDie()
    {
        // Здесь можно добавить эффект смерти, анимацию или звук
        Debug.Log("Player died!");
        Destroy(gameObject); // Уничтожаем объект врага
    }
}