using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public float launchSpeed = 5f;
    public float minLaunchSpeed = 5f;
    public float maxFlightTime = 3;

    public float scatterAngle = 5f; // ���� �������� � ��������

    public float fireRate = 1f; // ������� �������� � ��������� � �������
    public float nextFireTime = 0f; // ����� �� ���������� ��������

    void Update()
    {
        // ���������, ����� �� �������� � ����������� �� �������
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z = 0; // ������������, ��� Z-���������� ����� 0 ��� 2D

            GameObject Bullet = Instantiate(bullet, transform.position, Quaternion.identity);

            Vector2 direction = (mousePosition - transform.position).normalized;

            // ���������� ��������� ���� ��������
            float randomAngle = Random.Range(-scatterAngle, scatterAngle);
            // ����������� ���� � �������
            float angleInRadians = randomAngle * Mathf.Deg2Rad;
            // ������������ ����� ����������� � ������ ��������
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

            // ��������� ����� ���������� ��������
            nextFireTime = Time.time + 1f / fireRate; // �������� ����� ����������
        }
    }
}
