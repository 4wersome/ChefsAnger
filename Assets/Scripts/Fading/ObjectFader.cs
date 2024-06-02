using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectFader : MonoBehaviour {
    [SerializeField] private float fadeSpeed;
    [Range(1,0)] [SerializeField] private float fadeAmount;
    private float[] originalOpacity;
    private Material[] materials;
    private Coroutine fadeCoroutine;
    private bool doFade = false;
    public bool DoFade {
        get => doFade;
        set {
            if(doFade == value) return;
            doFade = value;
            Debug.Log("Hit Result: " + value);
            if(fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            if (value) fadeCoroutine = StartCoroutine(FadeCoroutine(fadeAmount));
            else fadeCoroutine = StartCoroutine(FadeCoroutine());
        }
    }

    private void Start() {
        materials = GetComponent<Renderer>().materials;
        originalOpacity = new float[materials.Length];
        for (int i = 0; i < materials.Length; i++) {
            originalOpacity[i] = materials[i].color.a;
        }
    }

    private IEnumerator FadeCoroutine(float opacity = 0) {
        if (doFade) {
            while (materials[0].color.a >= opacity) {
                for (int i = 0; i < materials.Length; i++) {
                    Color currentColor = materials[i].color;
                    float alpha = Mathf.Clamp01(currentColor.a - fadeSpeed * Time.deltaTime);
                    materials[i].color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                    Debug.Log("alpha "+ i +": " + materials[i].color.a);
                }
                yield return new WaitForEndOfFrame();
                
            }
        }
        else {

            while (materials[0].color.a <= originalOpacity[0]) {
                for (int i = 0; i < materials.Length; i++) {
                    Color currentColor = materials[i].color;
                    float alpha = Mathf.Clamp01(currentColor.a + fadeSpeed * Time.deltaTime);
                    materials[i].color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                    Debug.Log("alpha "+ i +": " + materials[i].color.a);
                }
                yield return new WaitForEndOfFrame();
            }
        }
        //StopCoroutine(fadeCoroutine);
    }
}
