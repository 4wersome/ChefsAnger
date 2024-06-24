using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Audiomngr : MonoBehaviour
{
   public static Audiomngr Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
    }

    public void PlayeOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
}
