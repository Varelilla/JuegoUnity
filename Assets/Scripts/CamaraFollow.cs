using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public Vector2 minValues, maxValues;  // Límites para la posición de la cámara

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        
        // Clampeo de la posición deseada dentro de los límites establecidos
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minValues.x, maxValues.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minValues.y, maxValues.y);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Si deseas que la cámara mire siempre al objetivo
        // transform.LookAt(target);
    }
}
