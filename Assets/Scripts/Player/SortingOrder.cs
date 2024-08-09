using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileChecker : MonoBehaviour
{
    public Tilemap tilemap; // ������ �� ��� Tilemap
    public Transform player; // ������ �� ������ ���������
    public float heightThreshold = 0; // ��������� ������ ��� ��������� ������� �����
    public float checkRadius = 1f; // ������ �������� ������� ������

    void Update()
    {
        CheckTilesInRadius();
    }

    void CheckTilesInRadius()
    {
        // �������� ������� ���������� ���������
        Vector3 playerPosition = player.position;

        // ����������� ������� ���������� � ���������� ������ Tilemap
        Vector3Int cellPosition = tilemap.WorldToCell(playerPosition);

        // ��������� ����� � �������� ������ ���������
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
                    Debug.Log($"���� ������ �� ������� {offsetPosition} � ��������� �������: {tileAtCell.name}");
                }
            }
        }

    }

    void UpdateSortingOrder(int cellYPosition)
    {
        // �������� ��������� TilemapRenderer
        TilemapRenderer tilemapRenderer = tilemap.GetComponent<TilemapRenderer>();

        // �������� sortingOrder � ����������� �� ������� ������
        if (cellYPosition > heightThreshold)
        {
            tilemapRenderer.sortingOrder = 1; // �����������
        }
        else
        {
            tilemapRenderer.sortingOrder = 0; // ��������� �������
        }
    }
}