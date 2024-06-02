using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVelocity3DAction : StateAction {
    private Rigidbody rigidbody;
    private Vector3 directionToSet;
    private float velocityLenghtToSet;
    private bool everyFrame;

    public SetVelocity3DAction(Rigidbody rigidbody, Vector3 directionToSet, float velocityLenghtToSet, bool everyFrame) {
        this.rigidbody = rigidbody;
        this.directionToSet = directionToSet;
        this.everyFrame = everyFrame;
        this.velocityLenghtToSet = velocityLenghtToSet;
    }
    
    public SetVelocity3DAction(Rigidbody rigidbody, Vector3 ownerTransform, Vector3 targetTransform, float velocityLenghtToSet, bool everyFrame) {
        this.rigidbody = rigidbody;
        this.directionToSet = (ownerTransform - targetTransform).normalized;
        this.everyFrame = everyFrame;
        this.velocityLenghtToSet = velocityLenghtToSet;
    }

    public override void OnEnter() {
        InternalSetVelocity();
    }

    public override void OnUpdate() {
        if (!everyFrame) return;
        InternalSetVelocity();
    }


    private void InternalSetVelocity () {
        rigidbody.velocity = directionToSet * velocityLenghtToSet;
    }
}