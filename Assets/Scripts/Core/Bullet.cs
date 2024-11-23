using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameManager.FoodType typeName; // Define el tipo de bala.

    private void OnEnable()
    {
        // Desactivar la bala después de un tiempo.
        Invoke(nameof(Deactivate), 5f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Deactivate()
    {
        GameManager.instance.GetBulletPool().ReturnBullet(typeName, gameObject);
    }
}

