using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource BgmAudio;
    [SerializeField] private AudioSource SfxAudio;

    public AudioClip bgm;
    
    public AudioClip found;
    public AudioClip item;
    public AudioClip doorOpen;
    public AudioClip doorClose;
    
    
    // Start is called before the first frame update
    void Start()
    {
        BgmAudio.clip = bgm;
        BgmAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFound()
    {
        SfxAudio.clip = found;
        SfxAudio.Play();
    }

    public void PlayItem()
    {
        SfxAudio.clip = item;
        SfxAudio.Play();
    }

    public void PlayDoorClose()
    {
        SfxAudio.clip = doorClose;
        SfxAudio.Play();
    }

    public void PlayDoorOpen()
    {
        SfxAudio.clip = doorOpen;
        SfxAudio.Play();
    }
}
