using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

public class PlayerAnimatorMngr : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnimator;

    public void SetTriggerParameter (string name)

    {
        playerAnimator.SetTrigger(Animator.StringToHash(name));
        
    }

    public void SetAnimatorBool(string name , bool value )
    {
        playerAnimator.SetBool(Animator.StringToHash(name), value);
    }

    public void SetAnimatorFloat (string name , float value )
    {
        playerAnimator.SetFloat(Animator.StringToHash(name), value);
        
    }

    //This check if an animation is currently running
    public bool CheckCurrentAnimationState (int layer , string StateName)
    {
        return playerAnimator.GetCurrentAnimatorStateInfo(layer).IsName(StateName);
    }

    public void AnimateBlendTree(string XparameterName, float X , string YparameterName,float Y )
    {
        playerAnimator.SetFloat(Animator.StringToHash(XparameterName), X);
        playerAnimator.SetFloat(Animator.StringToHash(YparameterName), Y);

    }
}
