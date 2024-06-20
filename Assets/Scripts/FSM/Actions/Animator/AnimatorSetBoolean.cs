using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSetBoolean : StateAction {

    private Animator animator;
    private bool valueToSet;

    public AnimatorSetBoolean(Animator animator, string parameterName, bool valueToSet, bool setOnEnter = true, bool setOnExit = true) {
        this.animator = animator;
        this.valueToSet = valueToSet;
        this.setOnEnter = setOnEnter;
        this.setOnExit = setOnExit;
        this.parameterName = parameterName;
    }

    private bool setOnEnter;
    private bool setOnExit;
    private string parameterName;


    public override void OnEnter() {
        if (setOnEnter) InternalSet(valueToSet);
    }

    public override void OnExit() {
        if (setOnExit) InternalSet(!valueToSet);
    }

    private void InternalSet(bool value) {
        animator.SetBool(Animator.StringToHash(parameterName), value);
    }
}
