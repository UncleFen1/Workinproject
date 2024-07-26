using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public float launchSpeed = 5f;
    public float minLaunchSpeed = 5f;
    public float maxFlightTime = 3;

    public float scatterAngle = 5f; // Угол разброса в градусах

    public float fireRate = 1f; // Частота стрельбы в выстрелах в секунду
    public float nextFireTime = 0f; // Время до следующего выстрела

    void Update()
    {
        // Проверяем, можно ли стрелять в зависимости от времени
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z = 0; // Обеспечиваем, что Z-координата равна 0 для 2D

            GameObject Bullet = Instantiate(bullet, transform.position, Quaternion.identity);

            Vector2 direction = (mousePosition - transform.position).normalized;

            // Генерируем случайный угол разброса
            float randomAngle = Random.Range(-scatterAngle, scatterAngle);
            // Преобразуем угол в радианы
            float angleInRadians = randomAngle * Mathf.Deg2Rad;
            // Рассчитываем новое направление с учетом разброса
            Vector2 scatterDirection = new Vector2(
                direction.x * Mathf.Cos(angleInRadians) - direction.y * Mathf.Sin(angleInRadians),
                direction.x * Mathf.Sin(angleInRadians) + direction.y * Mathf.Cos(angleInRadians)
            );

            Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float speed = launchSpeed;
                if (scatterDirection.magnitude < minLaunchSpeed)
                {
                    scatterDirection = scatterDirection.normalized * minLaunchSpeed;
                }
                rb.velocity = scatterDirection * speed;
            }
            Destroy(Bullet, maxFlightTime);

            // Обновляем время следующего выстрела
            nextFireTime = Time.time + 1f / fireRate; // Задержка между выстрелами
        }
    }
}
