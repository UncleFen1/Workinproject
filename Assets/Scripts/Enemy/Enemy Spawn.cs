using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;  // ������ �������� �����������
    public int enemyCount = 10;         // ���������� ����������� ��� ���������
    public Vector2 spawnAreaSize = new Vector2(10, 10); // ������ �������, ��� ����� ���������� ����������

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            // ���������� ��������� ���������� � �������� �������� �������
            Vector2 randomPosition = new Vector2(
                transform.position.x + Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                transform.position.y + Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
            );

            // �������� ��������� ������ ����������
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // ������� ��������� ���������� �� ��������� �������
            Instantiate(randomEnemyPrefab, randomPosition, Quaternion.identity);
        }
    }
}