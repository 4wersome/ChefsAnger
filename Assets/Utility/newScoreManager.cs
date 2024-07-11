using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class newScoreManager : MonoBehaviour
{
    private const string PlayerPrefCurrentScore = "Score";

    public static newScoreManager instance;

   
    private Text scoreText;
    int score = 0;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    
            SceneManager.sceneLoaded += OnSceneLoaded;
        
    }
    void Start()
    {
        if (scoreText != null)

            scoreText.text = "SCORE: " + score.ToString();
    }


    public void AddPoint()
    {
        score += 1;

        if (scoreText != null)
            scoreText.text = "SCORE: " + score.ToString();

        PlayerPrefs.SetInt(PlayerPrefCurrentScore, score);
    }
    public void updateScoreText(Text textToUpdate, bool isHighscore)
    {
        if (!isHighscore)
        {
        textToUpdate.text ="SCORE: " + score.ToString();
        }
        else
        {
            textToUpdate.text = "HIGHSCORE: " + PlayerPrefs.GetInt($"LeaderScore0").ToString();
        }
    }
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        PlayerPrefs.SetInt(PlayerPrefCurrentScore, 0);
        score = 0;
    }


}
