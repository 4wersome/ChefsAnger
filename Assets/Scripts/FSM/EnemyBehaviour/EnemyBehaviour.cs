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
    
    #region MonoBehaviour
    void Start() {
        StateMachine stateMachine = GetComponent<StateMachine>();
        
        State follow = SetUpBaseMovementState();
        State attack = SetUpAttackState();
        State death = SetUpDeathState();
        
        follow.SetUpMe(new Transition[] { FollowToAttack(follow, attack), StateToDeath(follow, death)});
        attack.SetUpMe(new Transition[] { AttackToFollow(attack, follow), StateToDeath(attack, death)});
        death.SetUpMe(new Transition[] { DeathToMovementState(death, follow)});
        stateMachine.Init(new State[] { follow, attack, death }, follow);
    }
    #endregion

    #region StateSetUp
    protected State SetUpBaseMovementState(){
        State state = new State();
        
        Animator animator = GetComponent<Animator>();
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        AnimatorSetSpeedAction setSpeedAction;
        AnimatorTriggerAction setTrigger = new AnimatorTriggerAction(animator, "Run");
        SetVelocity3DAction setVelocity;
        DebugAction debugAction = new DebugAction("MovementEnter", "MovementExit");

        if (Player.Get()) {
            setVelocity = new SetVelocity3DAction(rigidbody, Player.Get().transform, transform, followSpeed);
            setSpeedAction = new AnimatorSetSpeedAction(animator, rigidbody, "Speed", true);
            FollowTargetAction followTargetAction = new FollowTargetAction(gameObject, Player.Get().gameObject.transform);
            state.SetUpMe(new StateAction[] { setVelocity, followTargetAction, setSpeedAction, setTrigger, debugAction });
        }
        else {
            setVelocity = new SetVelocity3DAction(rigidbody, Vector3.zero, followSpeed);
            setSpeedAction = new AnimatorSetSpeedAction(animator, rigidbody, "Speed");
            state.SetUpMe(new StateAction[] { setVelocity, setSpeedAction, setTrigger, debugAction });
        }
        
        return state;
    }

    protected State SetUpAttackState(){
        State state = new State();
        SetVelocity2DAction stopAction = new SetVelocity2DAction(GetComponent<Rigidbody>(), Vector3.zero, false);
        
        Animator animator = GetComponent<Animator>();
        AnimatorTriggerAction setTrigger = new AnimatorTriggerAction(animator, "Attack");
        DebugAction debugAction = new DebugAction("AttackEnter", "AttackExit");

        state.SetUpMe(new StateAction[] { stopAction, setTrigger, debugAction });
        return state;
    }
    
    protected State SetUpDeathState(){
        State state = new State();
        SetVelocity2DAction stopAction = new SetVelocity2DAction(GetComponent<Rigidbody>(), Vector3.zero, false);
        
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
        //to implement an exit after the animation
        return ShiftStateOnPlayerDistance(follow, attack, COMPARISON.LESSEQUAL, distanceToStartAttack);
    }
    
    protected virtual Transition AttackToFollow(State attack, State follow) {
        return ShiftStateOnPlayerDistance(follow, attack, COMPARISON.GREATEREQUAL, distanceToStopAttack);
    }

    protected virtual Transition StateToDeath(State actualState, State death) {
        Transition transition = new Transition();
        HpStateCondition hpStateCondition =
            new HpStateCondition(GetComponent<EnemyComponent>()?.HealthModule, COMPARISON.LESSEQUAL, 0);
        transition.SetUpMe(actualState, death, new Condition[]{ hpStateCondition });
        return transition;
    }

    protected virtual Transition DeathToMovementState(State death, State movemenState) {
        Transition transition = new Transition();
        HpStateCondition hpStateCondition =
            new HpStateCondition(GetComponent<EnemyComponent>()?.HealthModule, COMPARISON.GREATEREQUAL, 1);
        transition.SetUpMe(death, movemenState, new Condition[]{ hpStateCondition });
        return transition;
    }
    
    #endregion
}