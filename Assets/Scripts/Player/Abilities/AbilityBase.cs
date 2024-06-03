using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    [SerializeField]
    protected RecipeNameEnum requiredRecipe;

    protected PlayerController playerController;
    protected bool isPrevented = false;

    public RecipeNameEnum RequiredRecipe { get { return requiredRecipe; }}
    public bool IsPrevented {  get { return isPrevented; } }


    public  void Init(PlayerController controller)
    {
        playerController = controller;
    }

    protected abstract void PreventAbility();
    protected abstract void UnPreventAbility();

    public virtual void UnlockAbility(){
        Debug.Log("Ability Enabled: " + this);
    }
}
