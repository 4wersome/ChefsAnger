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
        if (setOnEnter) InternalSet(true);
        else InternalSet(false);
    }

    public override void OnExit() {
        if (setOnEnter) InternalSet(false);
        else InternalSet(true);
    }

    private void InternalSet(bool value) {
        animator.SetBool(Animator.StringToHash(parameterName), value);
    }
}
