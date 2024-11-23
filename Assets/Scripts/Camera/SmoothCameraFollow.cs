using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target; // El personaje o el objeto a seguir.
    public float smoothSpeed = 0.125f; // Velocidad de suavizado.
    public Vector3 offset; // Desplazamiento de la cámara respecto al objetivo.

    private Vector3 velocity = Vector3.zero; // Usado para SmoothDamp.

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
    }

}
