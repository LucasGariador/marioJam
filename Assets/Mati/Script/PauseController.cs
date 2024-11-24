using UnityEngine;

public class PauseController : MonoBehaviour
{
    private bool pause;
    [SerializeField] private GameObject uiPause;

    private void Start()
    {
        uiPause.SetActive(false); // Aseg�ra de que el men� de pausa est� oculto al inicio.
        pause = false; // Aseg�ra de que el juego no est� en pausa al inicio.
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
            uiPause.SetActive(true); // Muestra el men� de pausa.
        }
        else
        {
            Time.timeScale = 1.0f; // Restaura el tiempo del juego.
            uiPause.SetActive(false); // Oculta el men� de pausa.
        }
    }
}
