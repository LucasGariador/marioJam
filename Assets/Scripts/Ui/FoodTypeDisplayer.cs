using System;
using System.Collections;
using Unity.VisualScripting;
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
    private Sprite pochocloSP;

    [SerializeField]
    private Image foodContainer;
    [SerializeField]
    private Image foodImage;

    public bool isReducing = false; // Variable que activa el proceso.
    public float duration = 1f; // Duración en segundos para reducir el fill.
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
        // Activa la corutina cuando la variable se vuelva verdadera.
        if (isReducing)
        {
            foodContainer.fillAmount = 1;
            isReducing = false; // Evita que se llame repetidamente.
            StartCoroutine(ReduceFill());
        }

    }

    IEnumerator ReduceFill()
    {
        float elapsedTime = 0f;
        float initialFill = foodContainer.fillAmount; // Fill inicial (debería ser 1).

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            foodContainer.fillAmount = Mathf.Lerp(initialFill, 0f, elapsedTime / duration); // Interpolación suave.
            yield return null; // Espera al siguiente frame.
        }

        // Asegúrate de que el fill termine exactamente en 0.
        foodContainer.fillAmount = 0f;
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
            case GameManager.FoodType.Pochoclo:
                foodImage.sprite= pochocloSP;
                break;
            default:
                break;

        }
    }
}
