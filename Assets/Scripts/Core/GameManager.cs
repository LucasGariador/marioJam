using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Transform playerPosition;
    [SerializeField]
    private GameObject churro;
    [SerializeField]
    private GameObject pancho;
    [SerializeField]
    private GameObject choclo;

    private FoodTypeDisplayer foodTypeDisplayer;
    [HideInInspector]
    public int[] CurrentAmmo { get; private set; }

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
    private void Start()
    {
        foodTypeDisplayer = FindAnyObjectByType<FoodTypeDisplayer>();
        playerPosition = FindAnyObjectByType<PlayerController>().transform;

        CurrentAmmo = new int[Enum.GetNames(typeof(FoodType)).Length];
        foreach (FoodType ft in (FoodType[])Enum.GetValues(typeof(FoodType)))
        {
                CurrentAmmo[(int)ft] = FindAnyObjectByType<ShootPlayer>().maxAmmo;
        }
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

    public void HasShot(float fireRate)
    {
        foreach (FoodType ft in (FoodType[])Enum.GetValues(typeof(FoodType))) 
        {
            if (ft == currentFoodType)
                CurrentAmmo[(int)ft] -= 1;
        }
        foodTypeDisplayer.isReducing = true;
        foodTypeDisplayer.duration = fireRate;
    }

    public int GetAmmo() 
    {
        return CurrentAmmo[(int)currentFoodType];
    }

    public void StockAmmo()
    {
        CurrentAmmo[(int)currentFoodType] += 1;
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
