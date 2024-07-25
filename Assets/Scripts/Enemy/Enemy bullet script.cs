using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 20;                            // ���� ����


  
   
    void OnTriggerEnter2D(Collider2D other)
    {
        // ���������, ������ �� ���� � ������ � ����� "Enemy"
        if (other.CompareTag("Player"))
        {
            // �������� ������ �� ��������� �������� ���������� � ������� ����
            PlayerHealth PlayerHealth = other.GetComponent<PlayerHealth>(); // ����������� ��� ������ ��� �������� �����
            if (PlayerHealth != null)
            {
                PlayerHealth.TakePlayerDamage(damage); // ����� ��� ��������� �����
            }
            Destroy(gameObject); // ���������� ����
        }
    }
}