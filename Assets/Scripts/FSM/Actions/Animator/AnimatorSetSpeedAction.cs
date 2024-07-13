using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSetSpeedAction : AnimatorSetFloat {
    
    private Rigidbody owner;

    public AnimatorSetSpeedAction(Animator animator, Rigidbody owner, string parameterName, bool everyFrame = false) 
        : base(animator, owner.velocity.magnitude, parameterName, everyFrame) {
        this.owner = owner;
    }

    public override void OnEnter() {
        InternalSet();
    }

    public override void OnUpdate() {
        if (!everyFrame) return;
        InternalSet();
    }

    protected override void InternalSet() {
        animator.SetFloat(Animator.StringToHash(parameterName), owner.velocity.magnitude);
    }
}