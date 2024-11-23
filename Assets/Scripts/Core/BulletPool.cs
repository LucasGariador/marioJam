using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [System.Serializable]
    public class BulletType
    {
        public GameManager.FoodType foodtype;
        public Sprite sprite;
        public int initialSize;
    }

    public GameObject bulletPrefab; // Prefab genérico de la bala.
    public List<BulletType> bulletTypes; // Lista de tipos de balas.

    private Dictionary<GameManager.FoodType, Queue<GameObject>> poolDictionary = new Dictionary<GameManager.FoodType, Queue<GameObject>>();

    void Start()
    {
        foreach (var bulletType in bulletTypes)
        {
            Queue<GameObject> bulletQueue = new Queue<GameObject>();
            for (int i = 0; i < bulletType.initialSize; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.GetComponent<Bullet>().typeName = bulletType.foodtype;
                bullet.SetActive(false);
                bullet.GetComponent<SpriteRenderer>().sprite = bulletType.sprite;
                bulletQueue.Enqueue(bullet);
            }
            poolDictionary.Add(bulletType.foodtype, bulletQueue);
        }
    }

    public GameObject GetBullet(GameManager.FoodType foodType, Vector2 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(foodType))
        {
            Debug.LogWarning($"Bullet type '{foodType}' not found in the pool!");
            return null;
        }

        GameObject bullet;
        if (poolDictionary[foodType].Count > 0)
        {
            bullet = poolDictionary[foodType].Dequeue();
        }
        else
        {
            // Crear una nueva bala si el pool está vacío.
            BulletType bulletType = bulletTypes.Find(t => t.foodtype == foodType);
            bullet = Instantiate(bulletPrefab);
            bullet.GetComponent<Bullet>().typeName = foodType;
            bullet.GetComponent<SpriteRenderer>().sprite = bulletType.sprite;
        }

        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        bullet.SetActive(true);

        return bullet;
    }

    public void ReturnBullet(GameManager.FoodType foodType, GameObject bullet)
    {
        bullet.SetActive(false);
        if (!poolDictionary.ContainsKey(foodType))
        {
            Debug.LogWarning($"Bullet type '{foodType}' not found in the pool!");
            Destroy(bullet); // Eliminar la bala si el tipo no existe en el pool.
            return;
        }

        poolDictionary[foodType].Enqueue(bullet);
    }
}
