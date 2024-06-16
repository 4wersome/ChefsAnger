using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSetFloat : StateAction {
    protected Animator animator;
    protected float floatValue;
    protected string parameterName;
    protected bool everyFrame;

    public AnimatorSetFloat(Animator animator, float floatValue, string parameterName, bool everyFrame = false) {
        this.animator = animator;
        this.floatValue = floatValue;
        this.parameterName = parameterName;
        this.everyFrame = everyFrame;
    }

    public override void OnEnter() {
        InternalSet();
    }

    public override void OnUpdate() {
        if(!everyFrame) return;
        InternalSet();
    }

    protected virtual void InternalSet() {
        animator.SetFloat(Animator.StringToHash(parameterName), floatValue);
    }
}
