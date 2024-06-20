using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentFollowAction : StateAction {
    protected NavMeshAgent agent;
    protected Transform target;
    private bool targetIsStatic;

    public NavAgentFollowAction(NavMeshAgent agent, Transform target, bool targetIsStatic = false) {
        this.agent = agent;
        this.target = target;
        this.targetIsStatic = targetIsStatic;
    }

    public override void OnEnter() {
        InternalSetSpeed();
    }

    public override void OnUpdate() {
        if(targetIsStatic)return;
        InternalSetSpeed();
    }

    public override void OnExit() {
        agent.ResetPath();
    }

    private void InternalSetSpeed() {
        agent.SetDestination(target.position);
    }
}
