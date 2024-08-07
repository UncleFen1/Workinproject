using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // ���� ��������
    private Vector3 originalRotation;
    private Quaternion targetRotation;

    void Start()
    {
        // ��������� �������� �������� �������
        originalRotation = transform.rotation.eulerAngles;
    }

    void Update()
    {
        // �������� ������� ������� "R" ��� �������� �� 180 ��������
        if (Input.GetKeyDown(KeyCode.A))
        {
            targetRotation = Quaternion.Euler(originalRotation.x, originalRotation.y + 180, originalRotation.z);
            transform.rotation = targetRotation;
        }

        // �������� ������� ������� "E" ��� �������� � �������� ���������
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(originalRotation);
        }
    }
}