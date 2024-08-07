using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectX : MonoBehaviour
{
    // Угол вращения
    private Vector3 originalRotation;
    private Quaternion targetRotation;

    void Start()
    {
        // Сохраняем исходное вращение объекта
        originalRotation = transform.rotation.eulerAngles;
    }

    void Update()
    {
        // Проверка нажатия клавиши  для поворота на 180 градусов
        if (Input.GetKeyDown(KeyCode.A))
        {
            targetRotation = Quaternion.Euler(originalRotation.x + 180, originalRotation.y, originalRotation.z);
            transform.rotation = targetRotation;
        }

        // Проверка нажатия клавиши  для возврата в исходное положение
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(originalRotation);
        }
    }
}