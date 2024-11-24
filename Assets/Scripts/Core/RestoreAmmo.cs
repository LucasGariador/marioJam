using System.Collections;
using UnityEngine;

public class RestoreAmmo : MonoBehaviour
{
    [SerializeField]
    private float restockingSpeed = 1.2f;
    
    private ShootPlayer player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponentInChildren<ShootPlayer>();
            StartCoroutine(nameof(StartRestocking));
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopCoroutine(nameof(StartRestocking));
        }
    }

    IEnumerator StartRestocking()
    {
        while (true)
        {
            if (GameManager.instance.GetAmmo() < player.maxAmmo)
            {
                GameManager.instance.StockAmmo();
                Debug.Log("Socking!!!!");
            }
            else
            {
                Debug.Log("Full");
            }

            yield return new WaitForSeconds(restockingSpeed);
        }
    }
}
