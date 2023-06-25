using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Цель, за которой будет следовать камера
    public float smoothSpeed = 0.125f; // Сглаживание движения камеры
    public float aheadDistance = 2f; // Расстояние, на котором камера находится впереди

    private Vector3 offset; // Смещение камеры относительно цели

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + target.forward * aheadDistance;
        targetPosition.y = transform.position.y; // Задаем текущую позицию по оси Y
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(target);
    }
}
