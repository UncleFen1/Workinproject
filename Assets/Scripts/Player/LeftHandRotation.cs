using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsMouseLeft : MonoBehaviour
{
    public Transform pivot; // “очка, вокруг которой будет происходить вращение
    public Camera mainCamera; //  амера, с которой будет следитьс€ за курсором

    void Update()
    {
        RotateObject();
    }

    private void RotateObject()
    {
        // ѕолучаем позицию курсора на экране
        Vector3 mouseScreenPosition = Input.mousePosition;

        // ”станавливаем значение Z дл€ правильного преобразовани€
        mouseScreenPosition.z = mainCamera.nearClipPlane;

        // ѕреобразуем позицию курсора в мировые координаты
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        // ¬ычисл€ем вектор от pivot до позиции курсора
        Vector3 directionToCursor = (mouseWorldPosition - pivot.position).normalized;

        // ќпредел€ем угол вращени€ дл€ дочернего объекта
        float angle = Mathf.Atan2(directionToCursor.y, directionToCursor.x) * Mathf.Rad2Deg;

        // ”станавливаем поворот дочернего объекта так, чтобы его локальна€ ось указывала в сторону курсора
        // ќбратите внимание на 90 градусов - это зависит от ориентации вашего спрайта
        transform.rotation = Quaternion.Euler(0, 0, angle+ 75);

        // “еперь позици€ дочернего объекта будет оставатьс€ фиксированной относительно pivot,
        // поэтому ничто больше не мен€ет его местоположение.
    }
}
