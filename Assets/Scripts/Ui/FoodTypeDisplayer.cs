using System;
using UnityEngine;
using UnityEngine.UI;

public class FoodTypeDisplayer : MonoBehaviour
{
    [SerializeField]
    private GameManager.FoodType currentType;

    [SerializeField]
    private Sprite churroSP;
    [SerializeField]
    private Sprite panchoSP;
    [SerializeField]
    private Sprite chocloSP;

    [SerializeField]
    private Image foodImage;
    private void Start()
    {
        foodImage = GetComponent<Image>();
        currentType = GameManager.instance.GetCurrentFoodType();
        UpdateSprite();
    }

    void Update()
    {
        if(GameManager.instance.GetCurrentFoodType() != currentType)
        {
            currentType = GameManager.instance.GetCurrentFoodType();
            UpdateSprite();
        }
    }

    private void UpdateSprite()
    {
        switch (currentType)
        {
            case GameManager.FoodType.Churro:
                foodImage.sprite = churroSP;
                break;
            case GameManager.FoodType.Pancho:
                foodImage.sprite= panchoSP;
                break;
            case GameManager.FoodType.Choclo:
                foodImage.sprite= chocloSP;
                break;
            default:
                break;

        }
    }
}
