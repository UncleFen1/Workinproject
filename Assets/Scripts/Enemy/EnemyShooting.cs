using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform player;           // ������ �� ������ ������
    public GameObject bullet1;    // ������ ����
    public Transform shootingPoint;    // �����, ������ ����� �������� ����
    public float shootingRange = 10f;  // ��������� ��������
    public float fireRate = 1f;        // ������� ��������� (��������� � �������)

    private float nextFireTime = 1f;   // ����� �� ���������� ��������

    void Start()
    {
        // ����� ������ � ����� �� ����
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform; // ����������� ������ �� ������
        }
        else
        {
            Debug.LogWarning("����� �� ������. ���������, ��� � ���� ���������� ��� 'Player'.");
        }
    }
    void Update()
    {
        // ��������� ���������� �� ������
        if (player != null && Vector2.Distance(transform.position, player.position) <= shootingRange)
        {
            // ��������, ���� �� �������� �� �������
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate; // ��������� ��������� ����� ��������
            }
        }
    }

    void Shoot()
    {
        // ������� ����
        GameObject bullet = Instantiate(bullet1, shootingPoint.position, shootingPoint.rotation);

        // �������� ����������� � ������
        Vector2 direction = (player.position - shootingPoint.position).normalized;

        // �������� ��������� Rigidbody2D ���� � ��������� ���� ��� ��������
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * 10f; // ���������� �������� ���� �� ������ ����������
        }
    }
}