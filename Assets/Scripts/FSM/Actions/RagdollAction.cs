using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollAction : StateAction {
    private Animator animator;
    private Collider mainCollider;
    private Rigidbody mainRigidbody;
    private Rigidbody[] rigidbodies;
    private Collider[] colliders;

    public RagdollAction(Animator animator, Collider mainCollider, Rigidbody mainRigidbody, Rigidbody[] rigidbodies, Collider[] colliders) {
        this.animator = animator;
        this.mainCollider = mainCollider;
        this.mainRigidbody = mainRigidbody;
        this.rigidbodies = rigidbodies;
        this.colliders = colliders;
    }

    public override void OnEnter() {
        animator.enabled = false;
        SetRigidbodyState(false);
        SetColliderState(true);
    }

    public override void OnExit() {
        animator.enabled = true;
        SetRigidbodyState(true);
        SetColliderState(false);
    }

    #region PrivateMethods
    private void SetRigidbodyState(bool state) {
        foreach (Rigidbody rigidbody in rigidbodies) {
            rigidbody.isKinematic = state;
        }
        //mainRigidbody.isKinematic = !state;
        mainRigidbody.isKinematic = state;
    }

    private void SetColliderState(bool state) {
        foreach (Collider collider in colliders) {
            collider.enabled = state;
        }
        mainCollider.enabled = !state;
    }
    #endregion
}
