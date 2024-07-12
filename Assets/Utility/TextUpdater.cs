using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
    private const string PlayerPrefCurrentScore = "Score";

    [SerializeField]
    private Text TextToChange;
    [SerializeField]
    private bool isHighscore;


    // Update is called once per frame

    void Update()
    {
        newScoreManager.instance.updateScoreText(TextToChange, isHighscore);
    }
}
