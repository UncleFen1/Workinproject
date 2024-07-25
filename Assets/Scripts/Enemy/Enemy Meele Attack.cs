using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;


public class EnemyMeleeAttack : MonoBehaviour
{
    public float attackRange = 1f; // ��������� �����
    public int attackDamage = 20; // ����
    public float attackRate = 2f; // ������� ����
    private float nextAttackTime = 0f; // ����� ��������� ��������� �����

    public Transform player; // ������ �� ������
    public PlayerHealth playerHealth; // ������ �� ��������� �������� ������

    void Start()
    {
        // ������� ������ � ����� (�� ������ ������������ ������ ������ ��������� ������)
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            playerHealth = playerObject.GetComponent<PlayerHealth>(); // �������� ��������� PlayerHealth
        }
    }

    void Update()
    {
        // ���������, ���������� �� ����� � ���������� �� ����
        if (player != null && playerHealth != null)
        {
            // ��������� ���������� �� ������
            if (Vector2.Distance(transform.position, player.position) <= attackRange && Time.time >= nextAttackTime)
            {
                nextAttackTime = Time.time + 1f / attackRate; // ������������� ��������� ����� �����
                Attack();
            }

            // ���������, ��������� �� �����
            if (playerHealth.currentPlayerHealth <= 0)
            {
                player = null; // ������� ������ �� ������
                playerHealth = null; // ������� ������ �� ��������� ��������
            }
        }
    }

    void Attack()
    {
        // ���������, ���� ����� ��������� � �������� ���������
        if (player != null && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            // ������� ���� ������
            playerHealth.TakePlayerDamage(attackDamage); // ������������, ��� � ������ ���� ��������� PlayerHealth
        }
    }

    private void OnDrawGizmosSelected()
    {
        // ����������� ������� ����� � ���������
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}