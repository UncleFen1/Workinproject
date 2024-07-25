using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;          // ������ �� ������ ������
    public float speed = 2f;          // �������� �������� ����������
    public float detectionRange = 5f; // ��������� ����������� ������
    public float randomMovementTime = 2f;  // ����� ���������� ��������� �����������
    public float changeDirectionSpeed = 1f; // ���� ��������� �����������

    private Vector2 movementDirection;
    private float nextChangeTime;

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
        if (player != null)
        {
            // ���������� ���������� �� ������
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // ���� ����� � ������� �����������, �������� � ����
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
        // �������� ����������� � ������
        Vector2 direction = (player.position - transform.position).normalized;

        // ��������� � ������� ������
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction, speed * Time.deltaTime);
    }

    void RandomMovement()
    {
        // ������ ����������� ����� ������������ ���������� �������
        if (Time.time >= nextChangeTime)
        {
            movementDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            nextChangeTime = Time.time + randomMovementTime;
        }

        // ��������� � ��������� �����������
        transform.position += (Vector3)movementDirection * speed * Time.deltaTime;
    }
}