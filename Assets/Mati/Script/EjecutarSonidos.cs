using UnityEngine;

public class EjecutarSonidos : MonoBehaviour
{
    [SerializeField] private AudioClip mainMusic;

    void Start()
    {
        AudioManager.instance.PlayMusic(mainMusic);
    }

}
