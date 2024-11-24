using UnityEngine;

public class EjecutarSonidos : MonoBehaviour
{
    [SerializeField] private AudioClip mainMusic;

    [SerializeField] private AudioClip[] listaDeSFXDeArena;
    void Start()
    {
        AudioManager.instance.PlayMusic(mainMusic);
    }


    public void SonidoAleatorio()
    {
        if (listaDeSFXDeArena.Length == 0)
        {
            Debug.LogWarning("La lista de sonidos está vacía.");
            return;
        }

        // Elije un índice aleatorio
        int indiceAleatorio = Random.Range(0, listaDeSFXDeArena.Length);

        AudioManager.instance.PlaySFX(listaDeSFXDeArena[indiceAleatorio]);
    }
}
