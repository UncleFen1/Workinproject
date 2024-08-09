using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileChecker : MonoBehaviour
{
    public Tilemap tilemap; // Ссылка на ваш Tilemap
    public Transform player; // Ссылка на вашего персонажа
    public float heightThreshold = 0; // Пороговая высота для изменения порядка слоев
    public float checkRadius = 1f; // Радиус проверки наличия тайлов

    void Update()
    {
        CheckTilesInRadius();
    }

    void CheckTilesInRadius()
    {
        // Получаем мировые координаты персонажа
        Vector3 playerPosition = player.position;

        // Преобразуем мировые координаты в координаты ячейки Tilemap
        Vector3Int cellPosition = tilemap.WorldToCell(playerPosition);

        // Проверяем тайлы в квадрате вокруг персонажа
        bool tileFound = false;
        for (int x = -Mathf.CeilToInt(checkRadius); x <= Mathf.CeilToInt(checkRadius); x++)
        {
            for (int y = -Mathf.CeilToInt(checkRadius); y <= Mathf.CeilToInt(checkRadius); y++)
            {
                Vector3Int offsetPosition = cellPosition + new Vector3Int(x, y, 0);
                TileBase tileAtCell = tilemap.GetTile(offsetPosition);

                if (tileAtCell != null)
                {
                    tileFound = true;
                    Debug.Log($"Тайл найден на позиции {offsetPosition} в клеточной системе: {tileAtCell.name}");
                }
            }
        }

    }

    void UpdateSortingOrder(int cellYPosition)
    {
        // Получаем компонент TilemapRenderer
        TilemapRenderer tilemapRenderer = tilemap.GetComponent<TilemapRenderer>();

        // Изменяем sortingOrder в зависимости от позиции ячейки
        if (cellYPosition > heightThreshold)
        {
            tilemapRenderer.sortingOrder = 1; // Увеличиваем
        }
        else
        {
            tilemapRenderer.sortingOrder = 0; // Уменьшаем обратно
        }
    }
}