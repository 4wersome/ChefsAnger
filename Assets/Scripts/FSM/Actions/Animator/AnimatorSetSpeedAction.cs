using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSetSpeedAction : StateAction {
    private Animator animator;
    private Rigidbody owner;
    private string parameterName;
    private bool everyFrame;
    
    public AnimatorSetSpeedAction(Animator animator, Rigidbody owner, string parameterName, bool everyFrame = false) {
        this.animator = animator;
        this.owner = owner;
        this.parameterName = parameterName;
        this.everyFrame = everyFrame;
    }

    public override void OnEnter() {
        InternalSet();
    }

    public override void OnUpdate() {
        if (!everyFrame) return;
        InternalSet();
    }

    private void InternalSet () {
        animator.SetFloat(Animator.StringToHash(parameterName), owner.velocity.magnitude);
    }
}