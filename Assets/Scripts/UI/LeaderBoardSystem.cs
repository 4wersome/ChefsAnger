using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LeaderBoardSystem : MonoBehaviour
{
    private const string PlayerPrefsLastScoreName = "LastScore";

    private int leaderboardSize = 5;
    private int[] scores;
    private int lastScore;
   

    [SerializeField] private Text Leader0;
    [SerializeField] private Text Leader1;
    [SerializeField] private Text Leader2;
    [SerializeField] private Text Leader3;
    [SerializeField] private Text Leader4;

    private void Awake()
    {
        scores = new int[leaderboardSize];
        lastScore = PlayerPrefs.GetInt(PlayerPrefsLastScoreName);

    }

    private void Start()
    {
        LeaderBoardUpdate();
    }
    // Update is called once per frame
    void Update()
    {
        ShowLeaderBoard(); 
    }
    public void LeaderBoardUpdate()
    {


        for (int i = 0; i < leaderboardSize; i++)
        {
            scores[i] = PlayerPrefs.GetInt($"LeaderScore{i}");
        }
        Array.Reverse(scores);
        for (int i = 0; i < leaderboardSize; i++)
        {
            if (lastScore == scores[i])
            {
                break;
            }
            if (lastScore < scores[i])
            {
                if (i == 0)
                {
                    continue; 
                }

                else
                {
                    scores[0] = lastScore;

                }
            }

            if (lastScore > scores[scores.Length - 1])
            {
                scores[0] = lastScore;
                break;
            }

        }

        Array.Sort(scores);
        Array.Reverse(scores);
        for (int i = 0; i < leaderboardSize; i++)
        {
            PlayerPrefs.SetInt($"LeaderScore{i}", scores[i]);
        }


    }

    public void ShowLeaderBoard()
    {

        for (int i = 0; i < leaderboardSize; i++)
        {
            float tmpScore = PlayerPrefs.GetInt($"LeaderScore{i}");
            switch (i)
            {
                case 0:
                    Leader0.text = tmpScore.ToString();
                    break;
                case 1:
                    Leader1.text = tmpScore.ToString();
                    break;
                case 2:
                    Leader2.text = tmpScore.ToString();
                    break;
                case 3:
                    Leader3.text = tmpScore.ToString();
                    break;
                case 4:
                    Leader4.text = tmpScore.ToString();
                    break;
            }
        }
    }

}
