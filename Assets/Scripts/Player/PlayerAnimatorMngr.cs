using System.Collections;
using System.Collections.Generic;
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
}
