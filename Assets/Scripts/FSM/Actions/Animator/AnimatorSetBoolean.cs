using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSetBoolean : StateAction {

    private Animator animator;
    private bool setOnEnter;
    private string parameterName;

    public AnimatorSetBoolean(Animator animator, string parameterName, bool setOnEnter = true) {
        this.animator = animator;
        this.setOnEnter = setOnEnter;
        this.parameterName = parameterName;
    }

    public override void OnEnter() {
        if (setOnEnter) animator.SetBool(Animator.StringToHash(parameterName), setOnEnter);
        else animator.SetBool(Animator.StringToHash(parameterName), !setOnEnter);
    }

    public override void OnExit() {
        if (setOnEnter) animator.SetBool(Animator.StringToHash(parameterName), !setOnEnter);
        else animator.SetBool(Animator.StringToHash(parameterName), setOnEnter);
    }
}
