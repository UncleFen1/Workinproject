using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 20;                            // Урон пули


  
   
    void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, попала ли пуля в объект с тегом "Enemy"
        if (other.CompareTag("Player"))
        {
            // Получаем ссылку на компонент здоровья противника и наносим урон
            PlayerHealth PlayerHealth = other.GetComponent<PlayerHealth>(); // Используйте ваш скрипт для здоровья врага
            if (PlayerHealth != null)
            {
                PlayerHealth.TakePlayerDamage(damage); // Метод для нанесения урона
            }
            Destroy(gameObject); // Уничтожаем пулю
        }
    }
}