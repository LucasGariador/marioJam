using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{

    public void IniciarJuego()
    {
        SceneManager.LoadScene("Lucas"); // Cambiar a la escena donde va aestar todo el escenario
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }

    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void Atras()
    {
        SceneManager.LoadScene("MenuDeInicio");
    }
}
