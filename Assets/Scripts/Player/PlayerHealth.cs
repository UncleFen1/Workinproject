using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxPlayerHealth = 100; // ������������ ��������
    public int currentPlayerHealth;   // ������� ���� ��������

    void Start()
    {
        currentPlayerHealth = maxPlayerHealth; // ������������� ������� �������� �� ������������ �������� � ������
    }

    public void TakePlayerDamage(int damage)
    {
        currentPlayerHealth -= damage; // ��������� ������� �������� �� �������� �����
        Debug.Log("Player took damage: " + damage + " Current health: " + currentPlayerHealth);

        if (currentPlayerHealth <= 0)
        {
            PlayerDie(); // ���� �������� 0 ��� ������, �������� ����� ������
        }
    }

    void PlayerDie()
    {
        // ����� ����� �������� ������ ������, �������� ��� ����
        Debug.Log("Player died!");
        Destroy(gameObject); // ���������� ������ �����
    }
}