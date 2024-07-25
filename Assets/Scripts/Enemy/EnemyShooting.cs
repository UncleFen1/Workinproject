using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform player;           // Ссылка на объект игрока
    public GameObject bullet1;    // Префаб пули
    public Transform shootingPoint;    // Точка, откуда будут вылетать пули
    public float shootingRange = 10f;  // Дальность стрельбы
    public float fireRate = 1f;        // Частота выстрелов (выстрелов в секунду)

    private float nextFireTime = 1f;   // Время до следующего выстрела

    void Start()
    {
        // Поиск игрока в сцене по тегу
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform; // Присваиваем ссылку на игрока
        }
        else
        {
            Debug.LogWarning("Игрок не найден. Убедитесь, что у него установлен тег 'Player'.");
        }
    }
    void Update()
    {
        // Проверяем расстояние до игрока
        if (player != null && Vector2.Distance(transform.position, player.position) <= shootingRange)
        {
            // Проверка, пора ли стрелять по времени
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate; // Обновляем следующее время выстрела
            }
        }
    }

    void Shoot()
    {
        // Создаем пулю
        GameObject bullet = Instantiate(bullet1, shootingPoint.position, shootingPoint.rotation);

        // Получаем направление к игроку
        Vector2 direction = (player.position - shootingPoint.position).normalized;

        // Получаем компонент Rigidbody2D пули и добавляем силу для движения
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * 10f; // Установите скорость пули по вашему усмотрению
        }
    }
}