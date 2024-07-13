using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SerializedClass.VFXClass;
using UnityEngine;

public class VFXBaseBehavior : MonoBehaviour {

    #region SerializedField
    [SerializeField] private SerializedVFXItem[] vfxOnActions;
    [SerializeField] private PlayerController playerController;
    #endregion

    #region Mono
    private void Awake() {
        Type playerControllerType = playerController.GetType();
        foreach (SerializedVFXItem vfxOnAction in vfxOnActions) {
            PropertyInfo a = playerControllerType.GetProperty(vfxOnAction.actionName);
            
            Action action = (Action) a.GetValue(a);
            if(action == null) {
                Debug.LogWarning("property value " + vfxOnAction.actionName + " null");
                return;
            }
            
            
        }
    }
    #endregion
}