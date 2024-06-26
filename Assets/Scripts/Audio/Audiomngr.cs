using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.SceneManagement;
public class Audiomngr : MonoBehaviour
{
    private const string GameplaySceneName = "GamePlaySceneBACKUP";
    private const string MainMenuSceneName = "MainMenuScene";
    private const string GameOverSceneName = "GameOverScene";

    public static Audiomngr Instance { get; private set; }


    private EventInstance musicEventInstance;
    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
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
