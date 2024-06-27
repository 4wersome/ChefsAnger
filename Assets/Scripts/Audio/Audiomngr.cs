using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class Audiomngr : MonoBehaviour
{
    private const string GameplaySceneName = "GamePlaySceneBACKUP";
    private const string MainMenuSceneName = "MainMenuScene";
    private const string GameOverSceneName = "GameOverScene";


    [Header("Volume")]
    [Range(0, 1)]
    public float mastervolume = 1f;
    [Range(0, 1)]
    public float MusicVolume = 1f;
    [Range(0, 1)]
    public float SFXVolume = 1f;

    private Bus MasterBus;
    private Bus MusicBus;
    private Bus SfxBus;





    public static Audiomngr Instance { get; private set; }


    private EventInstance musicEventInstance;
    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;

        MasterBus = RuntimeManager.GetBus("bus:/");
        MusicBus = RuntimeManager.GetBus("bus:/Music");
        SfxBus = RuntimeManager.GetBus("bus:/SFX");


    }

    private void Update()
    {
        MasterBus.setVolume(mastervolume);
        MusicBus.setVolume(MusicVolume);
        SfxBus.setVolume(SFXVolume);

    }

    public void PlayeOneShot(EventReference sound, Vector3 worldPos)
    {
        if (Instance == null) return;
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        
        EventInstance instance = RuntimeManager.CreateInstance(eventReference);
        return instance;
    }

    public void InitializeMusic(EventReference musicReference)
    {
        if (Instance == null) return;
        musicEventInstance = CreateEventInstance(musicReference);
        musicEventInstance.start();

    }

    public void StopMusic()
    {
        musicEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        StopMusic();
        switch (scene.name)
        {
            case GameplaySceneName:
                StopMusic();
                InitializeMusic(FMODEventMAnager.Instance.GameplayMusic);
                break;

            case MainMenuSceneName:
                StopMusic();
                InitializeMusic(FMODEventMAnager.Instance.MainMenuMusic);
                break;

            case GameOverSceneName:
                StopMusic();
                InitializeMusic(FMODEventMAnager.Instance.GameOverMusic);
                break;

        }
    }
}
