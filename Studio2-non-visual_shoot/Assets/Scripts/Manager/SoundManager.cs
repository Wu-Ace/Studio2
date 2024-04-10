using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager instance;

    [SerializeField] private AudioSource _musicSource, _sfxSource;
    private void Awake()
    {
        if (instance !=  null)
        {
            Destroy(this);
        }
        instance = this;
        EventManager.instance.onPlaySound += PlaySound;
    }
    public void PlaySound(AudioClip clip, float volume)
    {
        _sfxSource.PlayOneShot(clip, volume);
    }

}