using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour {

    #region SerializedField
    [SerializeField] private float followSpeed;
    [SerializeField] private float distanceToStartAttack;
    [SerializeField] private float distanceToStopAttack;
    #endregion

    private StateMachine stateMachine;
    #region MonoBehaviour
    void Start() {
        stateMachine = GetComponent<StateMachine>();
        
        State follow = SetUpBaseMovementState();
        follow.stateName = "FollowState";
        State attack = SetUpAttackState();
        attack.stateName = "AttackState";
        State death = SetUpDeathState();
        death.stateName = "DeathState";
        
        follow.SetUpMe(new Transition[] { FollowToAttack(follow, attack), StateToDeath(follow, death)});
        attack.SetUpMe(new Transition[] { AttackToFollow(attack, follow), StateToDeath(attack, death)});
        death.SetUpMe(new Transition[] { DeathToMovementState(death, follow)});
        stateMachine.Init(new State[] { follow, attack, death }, follow);
    }

    private void Update() {
        Debug.Log(stateMachine.CurrentState.stateName);
    }
    #endregion

    #region StateSetUp
    protected State SetUpBaseMovementState(){
        State state = new State();
        
        Animator animator = GetComponent<Animator>();
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        //AnimatorTriggerAction setTrigger = new AnimatorTriggerAction(animator, "Run");
        SetVelocity3DAction setVelocity = new SetVelocity3DAction(rigidbody, Player.Get().transform, transform, followSpeed);
        AnimatorSetSpeedAction setSpeedAction = new AnimatorSetSpeedAction(animator, rigidbody, "Speed");
        
        FollowTargetAction followTargetAction = new FollowTargetAction(gameObject, Player.Get().gameObject.transform);
        
        DebugAction debugAction = new DebugAction("MovementEnter", "MovementExit");
        
        state.SetUpMe(new StateAction[] { setVelocity, setSpeedAction, followTargetAction, debugAction });
        
        return state;
    }

    protected State SetUpAttackState(){
        State state = new State();
        
        SetVelocity3DAction stopAction = new SetVelocity3DAction(GetComponent<Rigidbody>(), Vector3.zero, followSpeed);

        Animator animator = GetComponent<Animator>();
        AnimatorTriggerAction setTrigger = new AnimatorTriggerAction(animator, "Attack");
        AnimatorSetBoolean setBoolean = new AnimatorSetBoolean(animator, "AttackEnded", false);
        AnimatorSetFloat setAnimatorSpeed = new AnimatorSetFloat(animator, 0, "Speed");
        
        DebugAction debugAction = new DebugAction("AttackEnter", "AttackExit");

        state.SetUpMe(new StateAction[] { stopAction, setAnimatorSpeed, setBoolean, setTrigger, debugAction });
        return state;
    }
    
    protected State SetUpDeathState(){
        State state = new State();
        SetVelocity3DAction stopAction = new SetVelocity3DAction(GetComponent<Rigidbody>(), Vector3.zero, followSpeed);
        
        Animator animator = GetComponent<Animator>();
        AnimatorTriggerAction triggerDeath = new AnimatorTriggerAction(animator, "Death");
        //to implement a time delay for the visibility (or a gradual as for the material)
        SetVisibleAction setVisible = new SetVisibleAction(gameObject);
        
        DebugAction debugAction = new DebugAction("DeathEnter", "DeathExit");
        
        state.SetUpMe(new StateAction[] { stopAction, triggerDeath, setVisible, debugAction });
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

    protected virtual Transition StateToDeath(State actualState, State death) {
        Transition transition = new Transition();
        HpStateCondition hpStateCondition = new HpStateCondition(GetComponent<EnemyComponent>()?.HealthModule, COMPARISON.LESSEQUAL, 0);
        transition.SetUpMe(actualState, death, new Condition[]{ hpStateCondition });
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