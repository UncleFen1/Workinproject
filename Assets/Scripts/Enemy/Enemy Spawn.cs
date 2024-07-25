using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;  // Массив префабов противников
    public int enemyCount = 10;         // Количество противников для генерации
    public Vector2 spawnAreaSize = new Vector2(10, 10); // Размер площади, где будут появляться противники

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            // Генерируем случайные координаты в пределах заданной площади
            Vector2 randomPosition = new Vector2(
                transform.position.x + Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                transform.position.y + Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
            );

            // Выбираем случайный префаб противника
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // Создаем экземпляр противника на случайной позиции
            Instantiate(randomEnemyPrefab, randomPosition, Quaternion.identity);
        }
    }
}