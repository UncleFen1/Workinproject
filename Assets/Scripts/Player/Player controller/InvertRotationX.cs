using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InvertRotationX : MonoBehaviour
{
    public Transform pivot; // Точка, относительно которой будет происходить отражение
    private Vector3 initialPosition; // Хранит начальную позицию объекта
    private Quaternion initialRotation; // Хранит начальную ориентацию объекта
    private bool hasFlipped; // Флаг, указывающий, был ли объект перевернут

    void Start()
    {
        // Сохраняем начальную позицию и ориентацию объекта
        initialPosition = transform.localPosition; // Изменяем на localPosition
        initialRotation = transform.localRotation; // Изменяем на localRotation
        hasFlipped = false; // Изначально объект не перевернут
    }

    void Update()
    {
        // Проверяем нажатие клавиши (например, пробела для выполнения отражения)
        if (Input.GetKey(KeyCode.A) && !hasFlipped)
        {
            Flip();
            hasFlipped = true; // Устанавливаем флаг после поворота
        }

        // Проверяем нажатие клавиши R для сброса состояния
        if (Input.GetKey(KeyCode.D))
        {
            ResetPosition();
            hasFlipped = false; // Сбрасываем флаг при возврате в исходное положение
        }
    }

    void Flip()
    {
        // Отражение объекта на 180 градусов относительно точки pivot
        transform.RotateAround(pivot.position, Vector3.right, 180f);

        // Для инвертирования местоположения объекта относительно родителя
        Vector3 directionToPivot = (transform.localPosition - pivot.localPosition).normalized;
        float distanceToPivot = Vector3.Distance(transform.localPosition, pivot.localPosition);

        // Устанавливаем новую позицию объекта, инвертируя его относительно pivot
        transform.localPosition = pivot.localPosition + directionToPivot * distanceToPivot;
    }

    void ResetPosition()
    {
        // Сбрасываем позицию и ориентацию объекта в исходное состояние
        transform.localPosition = initialPosition; // Изменяем на localPosition
        transform.localRotation = initialRotation; // Изменяем на localRotation
    }
}
