using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraBehaviour : MonoBehaviour
{

    [SerializeField]
    private CinemachineVirtualCamera gameplayCamera;
    [SerializeField]
    private CinemachineVirtualCamera deathCamera;

    [SerializeField]
    bool testbool;

    private CinemachineBasicMultiChannelPerlin cameraNoise;


    private UnityAction<GlobalEventArgs> DeathCameraUpdated;
    private UnityAction<GlobalEventArgs> GameplayCameraUpdated;
    private UnityAction<GlobalEventArgs> ShakeOnTakingDmg;


    void Start()
    {
        if (gameplayCamera != null)
        {
            cameraNoise = gameplayCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        DeathCameraUpdated += OnDeathCamerraUpdate;
        GameplayCameraUpdated += OnSpawnCameraUpdate;
        ShakeOnTakingDmg += ShakeCamera;
        GlobalEventManager.AddListener(GlobalEventIndex.CAMERAPlayerDeath, DeathCameraUpdated);
        GlobalEventManager.AddListener(GlobalEventIndex.CAMERAPlayerSpawn, GameplayCameraUpdated);
        GlobalEventManager.AddListener(GlobalEventIndex.CAMERAOnPlayerTakingDmg, ShakeOnTakingDmg);


    }
    private void OnSpawnCameraUpdate(GlobalEventArgs message)
    {
        deathCamera.gameObject.SetActive(false);
        gameplayCamera.gameObject.SetActive(true);
    }
    private void OnDeathCamerraUpdate(GlobalEventArgs message)
    {
        gameplayCamera.gameObject.SetActive(false);
        deathCamera.gameObject.SetActive(true);
    }

    public void ShakeCamera(GlobalEventArgs message )
    {

        float amplitude = 5f;
        float frequency = 5f;
        float duration = 0.5f;

        if (cameraNoise == null) return;

        StartCoroutine(Shake(amplitude, frequency, duration));

    }

    private IEnumerator Shake(float amplitude, float frequency, float duration)
    {
        Debug.Log("Shaking Camera");
        cameraNoise.m_AmplitudeGain = amplitude;
        cameraNoise.m_FrequencyGain = frequency;
        yield return new WaitForSeconds(duration);
        cameraNoise.m_AmplitudeGain = 0f;
        cameraNoise.m_FrequencyGain = 0f;
    }
}


