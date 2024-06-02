using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    
    protected PlayerController playerController;
    protected bool isPrevented = false;


    public bool IsPrevented {  get { return isPrevented; } }


    
    public  void Init(PlayerController controller)
    {
        playerController = controller;
    }

    protected abstract void PreventAbility();


    protected abstract void UnPreventAbility();
}
