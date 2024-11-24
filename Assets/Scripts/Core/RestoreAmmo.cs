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
            player = collision.GetComponent<ShootPlayer>();
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
            GameManager.instance.StockAmmo();
            Debug.Log("Socking!!!!");
            yield return new WaitForSeconds(restockingSpeed);
        }
    }
}
