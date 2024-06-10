using UnityEngine;

public class Simple2DPatrol : StateAction {

    private GameObject patroller;
    private Transform leftPosition;
    private Transform rightPosition;

    private Transform currentTransformToReach;
    private Rigidbody2D rigidbody;
    
    public Simple2DPatrol (GameObject patroller, Transform leftPosition, 
        Transform rightPosition) {
        this.patroller = patroller;
        this.leftPosition = leftPosition;
        this.rightPosition = rightPosition;
        rigidbody = patroller.GetComponent<Rigidbody2D>();
    }

    public override void OnEnter() {
        currentTransformToReach = patroller.transform.right.x > 0 ?
            rightPosition : leftPosition;
        InternalSetVelocity();
    }

    public override void OnUpdate() {
        InternalSetVelocity();
        Vector3 positionToReachLocal = patroller.transform.
            InverseTransformPoint(currentTransformToReach.position);
        if (positionToReachLocal.x < 0) {
            Switch();
        }
    }

    private void Switch() {
        currentTransformToReach = currentTransformToReach == leftPosition ? rightPosition : leftPosition;
        patroller.transform.Rotate(0, 180, 0);
    }

    private void InternalSetVelocity () {
        Vector2 direction = currentTransformToReach == rightPosition ? Vector2.right : Vector2.left;
        rigidbody.velocity = direction * Mathf.Abs(rigidbody.velocity.x);
    }

}
