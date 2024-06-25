using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
public class Audiomngr : MonoBehaviour
{
    public static Audiomngr Instance { get; private set; }


    private EventInstance musicEventInstance;
    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void PlayeOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance instance = RuntimeManager.CreateInstance(eventReference);
        return instance;
    }

    public void InitializeMusic(EventReference musicReference)
    {
        musicEventInstance = CreateEventInstance(musicReference);
        musicEventInstance.start();

    }

    public void StopMusic()
    {
        musicEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
