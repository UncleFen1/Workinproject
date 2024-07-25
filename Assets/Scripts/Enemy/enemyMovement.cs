using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;          // Ссылка на объект игрока
    public float speed = 2f;          // Скорость движения противника
    public float detectionRange = 5f; // Дальность обнаружения игрока
    public float randomMovementTime = 2f;  // Время случайного изменения направления
    public float changeDirectionSpeed = 1f; // Угол изменения направления

    private Vector2 movementDirection;
    private float nextChangeTime;

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
        if (player != null)
        {
            // Определяем расстояние до игрока
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Если игрок в радиусе обнаружения, движемся к нему
            if (distanceToPlayer <= detectionRange)
            {
                MoveTowardsPlayer();
            }
            else
            {
                RandomMovement();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        // Получаем направление к игроку
        Vector2 direction = (player.position - transform.position).normalized;

        // Двигаемся в сторону игрока
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction, speed * Time.deltaTime);
    }

    void RandomMovement()
    {
        // Меняем направление через определенные промежутки времени
        if (Time.time >= nextChangeTime)
        {
            movementDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            nextChangeTime = Time.time + randomMovementTime;
        }

        // Двигаемся в случайном направлении
        transform.position += (Vector3)movementDirection * speed * Time.deltaTime;
    }
}