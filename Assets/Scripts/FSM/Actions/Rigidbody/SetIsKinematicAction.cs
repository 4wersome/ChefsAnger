using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIsKinematicAction : StateAction {
    private Rigidbody rigidbody;
    private bool onEnter;

    public SetIsKinematicAction(Rigidbody rigidbody, bool onEnter) {
        this.rigidbody = rigidbody;
        this.onEnter = onEnter;
    }

    public override void OnEnter() {
        if (onEnter) InternalSet(true);
        else InternalSet(false);
    }

    public override void OnExit() {
        if (onEnter) InternalSet(false);
        else InternalSet(true);
    }

    private void InternalSet(bool value) {
        rigidbody.isKinematic = value;
    }
}
