using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallsRendering : MonoBehaviour
{
    public TilemapRenderer[] enableTilemaps; // Массив тайлмап рендереров для включения
    public TilemapRenderer[] disableTilemaps; // Массив тайлмап рендердеров для отключения

    void OnTriggerEnter2D(EdgeCollider2D other)
    {
        // Проверяем, что объект, с которым мы сталкиваемся, является нужным объектом
        if (other.CompareTag("Player")) // Измените "Player" на нужный вам тег
        {
            ToggleTilemaps(); // Переключаем состояние тайлмапов
        }
    }

    void ToggleTilemaps()
    {
        // Включаем или отключаем тайлмапы в зависимости от их текущего состояния
        foreach (TilemapRenderer tilemapRenderer in enableTilemaps)
        {
            tilemapRenderer.enabled = !tilemapRenderer.enabled; // Переключаем видимость тайлмапа
        }

        foreach (TilemapRenderer tilemapRenderer in disableTilemaps)
        {
            tilemapRenderer.enabled = !tilemapRenderer.enabled; // Переключаем видимость тайлмапа
        }
    }
}

