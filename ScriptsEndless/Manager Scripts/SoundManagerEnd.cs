using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerEnd : MonoBehaviour
{
    public static SoundManagerEnd instance;

    private AudioSource audioSourceEnd;

    public bool sound = true;
    void Awake()
    {
        MakeSingleton();
        audioSourceEnd = GetComponent<AudioSource>();
    }
    void MakeSingleton()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SoundOnOff()
    {
        sound = !sound;
    }

    public void PlaySoundFx(AudioClip clip, float volume)
    {
        if(sound)
            audioSourceEnd.PlayOneShot(clip, volume);
    }
}
