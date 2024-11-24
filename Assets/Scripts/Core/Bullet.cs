using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameManager.FoodType typeName; // Define el tipo de bala.
    int direction;

    private void Start()
    {
        direction = UnityEngine.Random.Range(0, 2);
        direction = direction == 0 ? 1 : -1;
    }
    private void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + Time.deltaTime * 50f * direction);
    }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyAI>().GetHit(typeName);
            Deactivate();
        }
    }
}

