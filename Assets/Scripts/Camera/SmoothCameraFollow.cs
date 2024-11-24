using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target; // El personaje o el objeto a seguir.
    public float smoothSpeed = 0.125f; // Velocidad de suavizado.
    public Vector3 offset; // Desplazamiento de la cámara respecto al objetivo.

    private Vector3 velocity = Vector3.zero; // Usado para SmoothDamp.

    [Header("Límites de la cámara")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        // Aplica los límites a la posición deseada
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

        // Interpola suavemente hacia la posición deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

}
