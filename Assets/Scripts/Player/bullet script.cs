using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 20;                            // ���� ����


  
   
    void OnTriggerEnter2D(Collider2D other)
    {
        // ���������, ������ �� ���� � ������ � ����� "Enemy"
        if (other.CompareTag("enemy"))
        {
            // �������� ������ �� ��������� �������� ���������� � ������� ����
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>(); // ����������� ��� ������ ��� �������� �����
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // ����� ��� ��������� �����
            }
            Destroy(gameObject); // ���������� ����
        }
    }
}