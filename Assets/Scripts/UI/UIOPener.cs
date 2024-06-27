using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIOPener : MonoBehaviour
{

    [SerializeField]
    GameObject PauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (InputManager.OpenPause())
        {

            if (PauseMenu.active)
            {
                PauseMenu.SetActive(false);
                Time.timeScale = 1.0f;
            }
            else
            {
                PauseMenu.SetActive(true);
                Time.timeScale =0f;
            }
        }
    }

    public void MenuButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(0);
    }
}
