using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionScripts : MonoBehaviour
{

    private const string PlayerPrefsGamepadEnabledName = "GamepadEnabled";
    private bool gamepadEnabled;

    private void Awake()
    {
        PlayerPrefs.SetInt(PlayerPrefsGamepadEnabledName, (gamepadEnabled ? 1 : 0));
    }
    public void OnValueChanged()
    {
       if (!gamepadEnabled)
        {
            gamepadEnabled = true;
            PlayerPrefs.SetInt(PlayerPrefsGamepadEnabledName, (gamepadEnabled ? 1 : 0));

            Debug.Log(gamepadEnabled
                );
        }
       else
        {
            gamepadEnabled = false;
            PlayerPrefs.SetInt(PlayerPrefsGamepadEnabledName, (gamepadEnabled ? 1 : 0));

            Debug.Log(gamepadEnabled
                );
        }
     
    }
}
