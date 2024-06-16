using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextExample : Recipe
{
    [SerializeField]
    private TMP_Text textQuest;

    private void Awake()
    {
        textQuest.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
