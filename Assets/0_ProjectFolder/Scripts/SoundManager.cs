using UnityEngine;
using DG.Tweening;


public class SoundManager : MonoBehaviour
{
    public Sound[] soundList;

    public string audioToTest;
    public Vector2 pitchRangeTest;
    public float fadeTimeTest;
    public KeyCode playkey, pauseKey, playPitch, fadeInKey, fadeOutKey, randomKey;

    private void Start()
    {
        for (int i = 0; i < soundList.Length; i++)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            soundList[i].SetupAudioSource(audioSource);

            if (soundList[i].playOnAwake)
            {
                audioSource.Play();
            }
        }
    }

    //Para debugear
    void Update()
    {
        if (Input.GetKeyDown(playkey))
        {
            PlaySound(audioToTest);
        }

        if (Input.GetKeyDown(pauseKey))
        {
            PauseSound(audioToTest);
        }

        if (Input.GetKeyDown(playPitch))
        {
            PlayRandomPitch(audioToTest, pitchRangeTest.x, pitchRangeTest.y);
        }

        if (Input.GetKeyDown(fadeInKey))
        {
            FadeSoundIn(audioToTest, fadeTimeTest);
        }

        if (Input.GetKeyDown(fadeOutKey))
        {
            FadeSoundOut(audioToTest, fadeTimeTest);
        }

        if (Input.GetKeyDown(randomKey))
        {
            PlayRandomSound();
        }
    }

    private Sound FindSound(string name)
    {
        for (int i = 0; i < soundList.Length; i++)
        {
            if (soundList[i].audioName == name)
            {
                return soundList[i];
            }
        }

        Debug.LogWarning($"<color=yellow>the sound with {name} desn't exists</color>");
        return null;
    }

    public void PlaySound(string name)
    {
        Sound sound = FindSound(name);

        if(sound == null)
        {
            return;
        }

        sound.audioSource.Play();
    }

    public void PlayRandomPitch(string name, float minPitch = -1, float maxPitch = 1.5f)
    {
        Sound sound = FindSound(name);

        if (sound == null)
        {
            return;
        }

        sound.audioSource.pitch = Random.Range(minPitch, maxPitch);
        sound.audioSource.Play();
    }

    public void PauseSound(string name)
    {
        Sound sound = FindSound(name);

        if (sound == null)
        {
            return;
        }

        sound.audioSource.Pause();
    }

    public void FadeSoundIn(string name, float fadeTime = 0.25f)
    {
        Sound sound = FindSound(name);

        if (sound == null)
        {
            return;
        }

        sound.audioSource.volume = 0;
        sound.audioSource.Play();
        sound.audioSource.DOFade(sound.volume, fadeTime);
    }

    public void FadeSoundOut(string name, float fadeTime = 0.25f)
    {
        Sound sound = FindSound(name);

        if (sound == null)
        {
            return;
        }

        sound.audioSource.DOFade(0, fadeTime)
            .OnComplete(sound.audioSource.Stop);
    }

    public void PlayRandomSound()
    {
        int random = Random.Range(0, soundList.Length);
        Sound sound = soundList[random];

        if(sound == null)
        {
            return;
        }

        PlaySound(sound.audioName);
    }
}
