using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODEventMAnager : MonoBehaviour
{
    [field : Header("Pan SFX")]
    [field : SerializeField]
    public EventReference panOnHit {  get; private set; }

    [field: Header("Player SFX")]
    [field: SerializeField]
    public EventReference PlayerRunning { get; private set; }
    [field: SerializeField]
    public EventReference PlayerDeath { get; private set; }

    [field: Header("Enemies SFX")]
    [field: SerializeField]
    public EventReference EnemyMeleeOnDeath { get; private set; }


    [field: Header("Music")]
    [field: SerializeField]
    public EventReference GameplayMusic { get; private set; }

    public static FMODEventMAnager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null) return;

        Instance = this;
        DontDestroyOnLoad(this);
    }

}
