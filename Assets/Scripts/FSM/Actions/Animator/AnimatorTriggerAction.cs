using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTriggerAction : StateAction
{
    private Animator animator;
    private string triggerName;
    private bool everyFrame;

    public AnimatorTriggerAction(Animator animator, string triggerName, bool everyFrame = false) {
        this.animator = animator;
        this.triggerName = triggerName;
        this.everyFrame = everyFrame;
    }
    public override void OnEnter() {
        InternalSetTrigger();
    }

    public override void OnUpdate() {
        if (!everyFrame) return;
        InternalSetTrigger();
    }

    public override void OnExit() {
        animator.ResetTrigger(Animator.StringToHash(triggerName));
    }

    private void InternalSetTrigger () {
        animator.SetTrigger(Animator.StringToHash(triggerName));
    }
}
