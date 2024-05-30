using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    
    protected PlayerController playerController;
    protected bool isPrevented;


    public bool IsPrevented {  get { return isPrevented; } }


    
    public  void Init(PlayerController controller)
    {
        playerController = controller;
    }
    
}
