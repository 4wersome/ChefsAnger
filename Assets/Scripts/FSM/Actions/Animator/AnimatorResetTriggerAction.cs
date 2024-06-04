using UnityEngine;

public class AnimatorResetTriggerAction : StateAction
{

    private Animator animator;
    private string triggerName;
    private bool everyFrame;

    public AnimatorResetTriggerAction(Animator animator, string triggerName, bool everyFrame = false) {
        this.animator = animator;
        this.triggerName = triggerName;
        this.everyFrame = everyFrame;
    }

    public override void OnEnter() {
        InternalResetTrigger();
    }

    public override void OnUpdate() {
        if (!everyFrame) return;
        InternalResetTrigger();
    }

    private void InternalResetTrigger() {
        animator.ResetTrigger(Animator.StringToHash(triggerName));
    }

}
