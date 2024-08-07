using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsMouseLeft : MonoBehaviour
{
    public Transform pivot; // �����, ������ ������� ����� ����������� ��������
    public Camera mainCamera; // ������, � ������� ����� ��������� �� ��������

    void Update()
    {
        RotateObject();
    }

    private void RotateObject()
    {
        // �������� ������� ������� �� ������
        Vector3 mouseScreenPosition = Input.mousePosition;

        // ������������� �������� Z ��� ����������� ��������������
        mouseScreenPosition.z = mainCamera.nearClipPlane;

        // ����������� ������� ������� � ������� ����������
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        // ��������� ������ �� pivot �� ������� �������
        Vector3 directionToCursor = (mouseWorldPosition - pivot.position).normalized;

        // ���������� ���� �������� ��� ��������� �������
        float angle = Mathf.Atan2(directionToCursor.y, directionToCursor.x) * Mathf.Rad2Deg;

        // ������������� ������� ��������� ������� ���, ����� ��� ��������� ��� ��������� � ������� �������
        // �������� �������� �� 90 �������� - ��� ������� �� ���������� ������ �������
        transform.rotation = Quaternion.Euler(0, 0, angle+ 75);

        // ������ ������� ��������� ������� ����� ���������� ������������� ������������ pivot,
        // ������� ����� ������ �� ������ ��� ��������������.
    }
}
