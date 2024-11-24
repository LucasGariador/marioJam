using UnityEngine;

public class PauseController : MonoBehaviour
{
    private bool pause;
    [SerializeField] private GameObject uiPause;

    private void Start()
    {
        uiPause.SetActive(false); // Asegúra de que el menú de pausa esté oculto al inicio.
        pause = false; // Asegúra de que el juego no esté en pausa al inicio.
        Time.timeScale = 1.0f; // Configura el tiempo del juego para que avance normalmente al inicio.
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause; // Cambia el estado de pausa al presionar las teclas.
        }

        if (pause)
        {
            Time.timeScale = 0.0f; // Pausa el tiempo del juego.
            uiPause.SetActive(true); // Muestra el menú de pausa.
        }
        else
        {
            Time.timeScale = 1.0f; // Restaura el tiempo del juego.
            uiPause.SetActive(false); // Oculta el menú de pausa.
        }
    }
}
