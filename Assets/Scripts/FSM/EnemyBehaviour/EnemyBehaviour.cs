using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour {

    #region SerializedField
    [SerializeField] private float followSpeed;
    [SerializeField] private float distanceToStartAttack;
    [SerializeField] private float distanceToStopAttack;
    [SerializeField][Range(0,5)] private float ragdollDuration = 1f;
    #endregion

    private StateMachine stateMachine;
    
    #region MonoBehaviour
    void Start() {
        stateMachine = GetComponent<StateMachine>();
        NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
        Animator animator = GetComponent<Animator>();
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (!agent) {
#if DEBUG
            Debug.Log("enemy NEED the NavMeshAgent to work. implement it");
#endif
            Destroy(this);
        }

        
        agent.speed = followSpeed;
        State follow = SetUpNavAgentMovementState(agent, animator);
        State attack = SetUpNavAgentAttackState(animator);
        State dying = SetUpNavAgentDyingState(animator, rigidbody);
        State death = SetUpNavAgentDeathState(animator);
        
        follow.stateName = "FollowState";
        attack.stateName = "AttackState";
        dying.stateName = "DyingState";
        death.stateName = "DeathState";


        follow.SetUpMe(new Transition[] { FollowToAttack(follow, attack), StateToDying(follow, dying) });
        attack.SetUpMe(new Transition[] { AttackToFollow(attack, follow), StateToDying(attack, dying) });
        dying.SetUpMe(new Transition[] { DyingToDeath(dying, death) });
        death.SetUpMe(new Transition[] { DeathToMovementState(death, follow) });
        stateMachine.Init(new State[] { follow, attack, dying, death }, follow);
    }
    #endregion
    
    
    #region NavAgentStates
    protected State SetUpNavAgentMovementState(NavMeshAgent agent, Animator animator){
        State state = new State();

        AnimatorSetFloat setAnimatorFloatAction = new AnimatorSetFloat(animator, agent.speed, "Speed");
        NavAgentFollowAction followTargetAction = new NavAgentFollowAction(agent, Player.Get().gameObject.transform);
#if DEBUG
        DebugAction debugAction = new DebugAction("MovementEnter", "MovementExit");
        state.SetUpMe(new StateAction[] { setAnimatorFloatAction, followTargetAction, debugAction });
#else
        state.SetUpMe(new StateAction[] { setAnimatorFloatAction, followTargetAction });
#endif
        return state;
    }
    
    protected State SetUpNavAgentAttackState(Animator animator){
        State state = new State();
        
        AnimatorTriggerAction setTrigger = new AnimatorTriggerAction(animator, "Attack");
        AnimatorSetBoolean setBoolean = new AnimatorSetBoolean(animator, "AttackEnded", false);
        AnimatorSetFloat setAnimatorSpeed = new AnimatorSetFloat(animator, 0, "Speed");
        
#if DEBUG
        DebugAction debugAction = new DebugAction("AttackEnter", "AttackExit");
        state.SetUpMe(new StateAction[] { setAnimatorSpeed, setBoolean, setTrigger, debugAction });
#else
        state.SetUpMe(new StateAction[] { setAnimatorSpeed, setBoolean, setTrigger });
#endif
        return state;
    }
    
    protected State SetUpNavAgentDyingState(Animator animator, Rigidbody mainRigidbody) {
        State state = new State();
        
        RagdollAction ragdollAction = new RagdollAction(animator, GetComponent<Collider>(), mainRigidbody,
            GetComponentsInChildren<Rigidbody>(), GetComponentsInChildren<Collider>());
        
        AnimatorSetBoolean setBoolean = new AnimatorSetBoolean(animator, "IsAlive", false, setOnExit: false);
        AnimatorTriggerAction triggerDeath = new AnimatorTriggerAction(animator, "Death");
        
#if DEBUG
        DebugAction debugAction = new DebugAction("DyingEnter", "DyingExit");
        state.SetUpMe(new StateAction[] { setBoolean, triggerDeath, ragdollAction, debugAction });
#else
        state.SetUpMe(new StateAction[] { setBoolean, triggerDeath, ragdollAction });
#endif
        return state;
    }
    
    protected State SetUpNavAgentDeathState(Animator animator){
        State state = new State();
        
        //AnimatorTriggerAction triggerDeath = new AnimatorTriggerAction(animator, "Death");
        
        //to implement a time delay for the visibility (or a gradual as for the material)
        AnimatorSetBoolean setBoolean = new AnimatorSetBoolean(animator, "IsAlive", false, false);
        SetVisibleAction setVisible = new SetVisibleAction(gameObject);

#if DEBUG
        DebugAction debugAction = new DebugAction("DeathEnter", "DeathExit");
        state.SetUpMe(new StateAction[] { setBoolean, setVisible, debugAction });
#else
        state.SetUpMe(new StateAction[] { setBoolean, setVisible });
#endif
        return state;
    }
    #endregion
    
    #region WithoutNavAgentState
    protected State SetUpMovementState(Animator animator, Rigidbody rigidbody){
        State state = new State();
        
        //AnimatorTriggerAction setTrigger = new AnimatorTriggerAction(animator, "Run");
        SetIsKinematicAction isKinematicAction = new SetIsKinematicAction(rigidbody, false);
        SetVelocity3DAction setVelocity = new SetVelocity3DAction(rigidbody, Player.Get().transform, transform, followSpeed, true);
        AnimatorSetSpeedAction setSpeedAction = new AnimatorSetSpeedAction(animator, rigidbody, "Speed");
        
        FollowTargetAction followTargetAction = new FollowTargetAction(gameObject, Player.Get().gameObject.transform);
#if DEBUG
        DebugAction debugAction = new DebugAction("MovementEnter", "MovementExit");
        state.SetUpMe(new StateAction[] { isKinematicAction, setVelocity, setSpeedAction, followTargetAction, debugAction });
#else
        state.SetUpMe(new StateAction[] { isKinematicAction, setVelocity, setSpeedAction, followTargetAction });
#endif
        
        
        return state;
    }
    
    protected State SetUpAttackState(Animator animator, Rigidbody rigidbody){
        State state = new State();

        SetVelocity3DAction stopAction = new SetVelocity3DAction(rigidbody, Vector3.zero, followSpeed);
        //SetIsKinematicAction isKinematicAction = new SetIsKinematicAction(rb, true);
        
        AnimatorTriggerAction setTrigger = new AnimatorTriggerAction(animator, "Attack");
        AnimatorSetBoolean setBoolean = new AnimatorSetBoolean(animator, "AttackEnded", false);
        AnimatorSetFloat setAnimatorSpeed = new AnimatorSetFloat(animator, 0, "Speed");
        
#if DEBUG
        DebugAction debugAction = new DebugAction("AttackEnter", "AttackExit");
        state.SetUpMe(new StateAction[] { stopAction, setAnimatorSpeed, setBoolean, setTrigger, debugAction });
#else
        state.SetUpMe(new StateAction[] {  stopAction, setAnimatorSpeed, setBoolean, setTrigger });
#endif
        return state;
    }
    
    protected State SetUpDeathState(Animator animator, Rigidbody rigidbody){
        State state = new State();
        SetVelocity3DAction stopAction = new SetVelocity3DAction(rigidbody, Vector3.zero, followSpeed);
        
        AnimatorTriggerAction triggerDeath = new AnimatorTriggerAction(animator, "Death");
        //to implement a time delay for the visibility (or a gradual as for the material)
        SetVisibleAction setVisible = new SetVisibleAction(gameObject);
#if DEBUG
        DebugAction debugAction = new DebugAction("DeathEnter", "DeathExit");
        state.SetUpMe(new StateAction[] { stopAction, triggerDeath, setVisible, debugAction });
#else
        state.SetUpMe(new StateAction[] { stopAction, triggerDeath, setVisible });
#endif
        return state;
    }
    #endregion


    #region TransitionSetUp
    protected virtual Transition ShiftStateOnPlayerDistance(State prev, State next, COMPARISON distanceComparison, float distanceToCheck) {
        Transition transition = new Transition();
        CheckDistanceCondition distanceCondition = new CheckDistanceCondition(transform, Player.Get().transform, distanceToCheck, distanceComparison);
        transition.SetUpMe(prev, next, new Condition[]{ distanceCondition });
        return transition;
    }
    
    protected virtual Transition FollowToAttack(State follow, State attack) {
        return ShiftStateOnPlayerDistance(follow, attack, COMPARISON.LESS, distanceToStartAttack);
    }
    
    protected virtual Transition AttackToFollow(State attack, State follow) {
        return ShiftStateOnPlayerDistance(attack, follow, COMPARISON.GREATER, distanceToStopAttack);
    }

    protected virtual Transition StateToDying(State actualState, State death) {
        Transition transition = new Transition();
        HpStateCondition hpStateCondition = new HpStateCondition(GetComponent<EnemyComponent>()?.HealthModule, COMPARISON.LESSEQUAL, 0);
        transition.SetUpMe(actualState, death, new Condition[]{ hpStateCondition });
        return transition;
    }

    protected virtual Transition DyingToDeath(State dying, State death) {
        Transition transition = new Transition();
        ExitTimeCondition exitTimeCondition = new ExitTimeCondition(ragdollDuration);
        transition.SetUpMe(dying, death, new Condition[] { exitTimeCondition });
        return transition;
    }
    
    protected virtual Transition DeathToMovementState(State death, State movemenState) {
        Transition transition = new Transition();
        HpStateCondition hpStateCondition = new HpStateCondition(GetComponent<EnemyComponent>()?.HealthModule, COMPARISON.GREATEREQUAL, 1);
        transition.SetUpMe(death, movemenState, new Condition[]{ hpStateCondition });
        return transition;
    }
    #endregion
}