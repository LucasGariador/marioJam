using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private Transform playerPosition;

    public enum FoodType
    {
        Churro,
        Pancho,
        Choclo
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
