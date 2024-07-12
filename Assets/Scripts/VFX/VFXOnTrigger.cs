using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXOnTrigger : MonoBehaviour {
    
    [SerializeField] private ParticleSystem VFX;
    [SerializeField] protected string[] compatibleTag;
    
    private void OnTriggerEnter(Collider other) {
        foreach (string tag in compatibleTag) {
            if (other.CompareTag(tag)) {
                VFX.Play();
                ParticleSystem.EmissionModule emissionModule = VFX.emission;
                emissionModule.enabled = true;
            }
        }
        

    }
}
