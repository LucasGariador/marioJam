using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private Transform playerPosition;
    [SerializeField]
    private GameObject churro;
    [SerializeField]
    private GameObject pancho;
    [SerializeField]
    private GameObject choclo;

    [SerializeField]
    BulletPool bulletPool;
    public enum FoodType
    {
        Churro,
        Pancho,
        Choclo
    }

    [SerializeField]
    private FoodType currentFoodType;

    private void Awake()
    {
        Physics.gravity = Vector2.zero;
    }

    public FoodType GetCurrentFoodType()
    {
        return currentFoodType;
    }

    public GameObject GetFoodSprite(FoodType food)
    {
        switch (food)
        {
            case FoodType.Churro:
                return churro;
            case FoodType.Pancho:
                return pancho;
            case FoodType.Choclo:
                return choclo;
            default: return null;
        }
    }

    public BulletPool GetBulletPool()
    {
        return bulletPool;
    }

    public void ChangeCurrentFoodType()
    {
        FoodType nextFood = currentFoodType + 1;
        if ((int)nextFood >= Enum.GetNames(typeof(FoodType)).Length)
        {
            nextFood = 0;
        }
        currentFoodType = nextFood;
    }
}
