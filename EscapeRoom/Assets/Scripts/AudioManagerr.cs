using UnityEngine;

public class AudioManagerr : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;

   
    public AudioClip[] gameSounds;
    public AudioClip[] gameMusic;  

  
    public static AudioManagerr instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(gameMusic!=null)
            PlayMusic(0);
    }

    // Function to play sound effect once
    public void PlaySFX(int index)
    {
        if (index >= 0 && index < gameSounds.Length)
        {
            sfxSource.PlayOneShot(gameSounds[index]);
        }
        else
        {
            Debug.LogWarning("Sound index out of range.");
        }
    }

    // Function to play looping background music
    public void PlayMusic(int index)
    {
        if (index >= 0 && index < gameMusic.Length)
        {
            musicSource.clip = gameMusic[index];
            musicSource.loop = true;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music index out of range.");
        }
    }
}
