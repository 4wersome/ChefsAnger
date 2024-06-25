using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusicManager : MonoBehaviour
{
    private const string GameplaySceneName = "GamePlaySceneBACKUP";
    private const string MainMenuSceneName = "MainMenuScene";
    private const string GameOverSceneName  = "GameOverScene";

    private void Awake()
    {

        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        Audiomngr.Instance.StopMusic();
        switch (scene.name)
        {
            case GameplaySceneName:
                Audiomngr.Instance.InitializeMusic(FMODEventMAnager.Instance.GameplayMusic);
                break;

            case MainMenuSceneName:
                Audiomngr.Instance.StopMusic();
                // Audiomngr.Instance.InitializeMusic(FMODEventMAnager.Instance.GameplayMusic);
                break;

            case GameOverSceneName:
                Audiomngr.Instance.StopMusic();
                // Audiomngr.Instance.InitializeMusic(FMODEventMAnager.Instance.GameplayMusic);
                break;

        }
    }
}
