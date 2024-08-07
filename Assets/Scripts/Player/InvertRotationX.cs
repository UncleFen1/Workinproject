using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectX : MonoBehaviour
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
        // �������� ������� �������  ��� �������� �� 180 ��������
        if (Input.GetKeyDown(KeyCode.A))
        {
            targetRotation = Quaternion.Euler(originalRotation.x + 180, originalRotation.y, originalRotation.z);
            transform.rotation = targetRotation;
        }

        // �������� ������� �������  ��� �������� � �������� ���������
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(originalRotation);
        }
    }
}