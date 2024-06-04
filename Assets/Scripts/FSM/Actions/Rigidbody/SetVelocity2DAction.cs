using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVelocity2DAction : StateAction
{

    private Rigidbody rigidbody;
    private Vector2 velocityToSet;
    private bool everyFrame;

    public SetVelocity2DAction (Rigidbody rigidbody, Vector2 velocityToSet, bool everyFrame) {
        this.rigidbody = rigidbody;
        this.velocityToSet = velocityToSet;
        this.everyFrame = everyFrame;
    }

    public override void OnEnter() {
        InternalSetVelocity();
    }

    public override void OnUpdate() {
        if (!everyFrame) return;
        InternalSetVelocity();
    }


    private void InternalSetVelocity () {
        rigidbody.velocity = velocityToSet;
    }

}
