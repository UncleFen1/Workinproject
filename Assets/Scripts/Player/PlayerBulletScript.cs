using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 20;                            // Урон пули


  
   
    void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, попала ли пуля в объект с тегом "Enemy"
        if (other.CompareTag("enemy"))
        {
            // Получаем ссылку на компонент здоровья противника и наносим урон
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>(); // Используйте ваш скрипт для здоровья врага
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // Метод для нанесения урона
            }
            Destroy(gameObject); // Уничтожаем пулю
        }
    }
}