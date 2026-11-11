using UnityEngine;

[System.Serializable] //anotación para que la clase sea visible en el inspector
public class Sound
{
    public string audioName;
    public AudioClip clip;

    [Range(0, 1)]
    public float volume;

    [Range(-3, 3)]
    public float pitch;

    public bool loop;
    public bool playOnAwake;

    public AudioSource audioSource;

    public void SetupAudioSource(AudioSource source)
    {
        audioSource = source;
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.loop = loop;
        audioSource.playOnAwake = playOnAwake;
    }
}
