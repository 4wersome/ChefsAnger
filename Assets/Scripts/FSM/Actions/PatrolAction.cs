using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAction : FollowTargetAction {
    [SerializeField] private Transform[] patrolTransforms;
    private int indexPatrol;
    protected int IndexPatrol{ get => indexPatrol;
        set {
            indexPatrol = value;
            if (indexPatrol > patrolTransforms.Length) indexPatrol = indexPatrol - (patrolTransforms.Length - 1);
        }
    }

    private const float distanceTreshold = 0.001f;
    
    public PatrolAction(GameObject owner, Transform[] patrolPositions, bool targetIsStatic = true, float rotationSpeed = 1) 
        : base(owner, patrolPositions[0], targetIsStatic, rotationSpeed) {
        patrolTransforms = patrolPositions;
    }
    public override void OnEnter() {
        indexPatrol = 0;
        base.OnEnter();
    }

    public override void OnUpdate() {
        if ((owner.transform.position - target.position).sqrMagnitude < distanceTreshold) {
            target = NextPosition();
            InternalSetVelocity();
        }
        else {
            base.OnUpdate();
        }
    }

    private Transform NextPosition(int increment = 1) {
        IndexPatrol += increment;
        return patrolTransforms[indexPatrol];
    }
}