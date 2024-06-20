using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectFader : MonoBehaviour {
    [Range(0.5f,10)][SerializeField] private float fadeSpeed = 5;
    [Range(1,0)] [SerializeField] private float fadeAmount = 0.2f;
    //private float[] originalOpacity;
    private const float originalOpacity = 1f;
    private Material[] materials;
    private Material[] fadingMaterials;
    private Coroutine fadeCoroutine;
    private MeshRenderer meshRenderer;
    private bool doFade = false;
    public bool DoFade {
        get => doFade;
        set {
            //Debug.Log("Hit Result: " + value);
            if(doFade == value || fadeSpeed <= 0) return;
            doFade = value;
            if(fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            if (value) fadeCoroutine = StartCoroutine(FadeCoroutine(fadeAmount));
            else fadeCoroutine = StartCoroutine(FadeCoroutine());
        }
    }

    private void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
        materials = new Material[meshRenderer.materials.Length];
        materials = meshRenderer.materials;
        fadingMaterials = new Material[materials.Length];
        for (int i = 0; i < materials.Length; i++) {
            fadingMaterials[i] = new Material(materials[i]);
            fadingMaterials[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            fadingMaterials[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            fadingMaterials[i].SetInt("_ZWrite", 0);
            fadingMaterials[i].DisableKeyword("_ALPHATEST_ON");
            fadingMaterials[i].EnableKeyword("_ALPHABLEND_ON");
            fadingMaterials[i].DisableKeyword("_ALPHAPREMULTIPLY_ON");
            fadingMaterials[i].renderQueue = 3000;
        }
    }

    private IEnumerator FadeCoroutine(float opacity = 0) {
        if (doFade) {
            //Debug.Log("Start Fade");
            SetMaterialRenderMode(true);
            while (fadingMaterials[0].color.a >= opacity) {
                for (int i = 0; i < fadingMaterials.Length; i++) {
                    
                    Color currentColor = fadingMaterials[i].color;
                    float alpha = Mathf.Clamp01(currentColor.a - fadeSpeed * Time.deltaTime);
                    fadingMaterials[i].color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                    //Debug.Log("alpha "+ i +": " + materials[i].color.a);
                }
                yield return new WaitForEndOfFrame();
                
            }
        }
        else {
            //Debug.Log("Start Visible");
            while (fadingMaterials[0].color.a < originalOpacity) {
                for (int i = 0; i < fadingMaterials.Length; i++) {
                    Color currentColor = fadingMaterials[i].color;
                    float alpha = Mathf.Clamp01(currentColor.a + fadeSpeed * Time.deltaTime);
                    fadingMaterials[i].color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                    //Debug.Log("alpha "+ i +": " + fadingMaterials[i].color.a);
                }
                yield return new WaitForEndOfFrame();
            }

            SetMaterialRenderMode(false);
        }
        //StopCoroutine(fadeCoroutine);
    }

    private void SetMaterialRenderMode(bool value) {
        if (value) {
            meshRenderer.materials = fadingMaterials;
        }
        else {
            meshRenderer.materials = materials;
        }
    }
}