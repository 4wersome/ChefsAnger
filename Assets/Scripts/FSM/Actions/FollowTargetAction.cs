using UnityEngine;

public class FollowTargetAction : StateAction {

    protected GameObject owner;
    protected Transform target;
    private float rotationSpeed;
    private bool targetIsStatic;
    private Rigidbody rigidbody;

    public FollowTargetAction(GameObject owner, Transform target, bool targetIsStatic = false, float rotationSpeed = 1) {
        this.owner = owner;
        this.target = target;
        this.targetIsStatic = targetIsStatic;
        if(rotationSpeed > 0f) this.rotationSpeed = rotationSpeed;
        rigidbody = owner.GetComponent<Rigidbody>();
    }

    public override void OnEnter() {
        InternalSetVelocity();
    }

    public override void OnUpdate () {
        if(targetIsStatic) return;
        InternalSetVelocity();
    }

    protected void InternalSetVelocity() {
        Vector3 direction = (target.transform.position - owner.transform.position).normalized;
        rigidbody.velocity = rigidbody.velocity.magnitude * direction;
        Quaternion rotation =  Quaternion.LookRotation(direction, Vector3.up);
        owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }
}