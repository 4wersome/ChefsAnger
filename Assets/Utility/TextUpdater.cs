using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
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
