using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{

    [SerializeField] private AudioSource musicSource;        // Para música
    [SerializeField] private AudioSource sfxSource;          // Para efectos de sonido

    public override void Init()
    {
        base.Init();
        Debug.Log("AudioManager Initialized");
    }
    
    public void PlayMusic(AudioClip musicClip, bool loop = true)
    {
        if (musicClip != null)
        {
            musicSource.clip = musicClip;
            musicSource.loop = loop;
            musicSource.Play();
        }
        else
        {
            print("No se asigno un clip de audio");
        }
    }

    public void PlaySFX(AudioClip sfxClip, float volume = 1.0f)
    {
        if (sfxSource != null)
        {
            // Variar el pitch aleatoriamente entre 0.9 y 1.1
            sfxSource.pitch = Random.Range(0.9f, 1.1f);

            // Reproducir el efecto de sonido
            sfxSource.PlayOneShot(sfxClip, volume);
        }
        else
        {
            Debug.LogWarning("SFX AudioSource is not assigned!");
        }
    }


}
