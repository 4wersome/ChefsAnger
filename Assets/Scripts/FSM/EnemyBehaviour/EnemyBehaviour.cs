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
        
        follow.SetUpMe(new Transition[] { FollowToAttack(follow, attack)});
        attack.SetUpMe(new Transition[] { AttackToFollow(attack, follow)});
        
        stateMachine.Init(new State[] { follow, attack }, follow);
    }
    #endregion

    #region StateSetUp
    protected State SetUpBaseMovementState(){
        State state = new State();
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        SetVelocity3DAction setVelocity = new SetVelocity3DAction(GetComponent<Rigidbody>(), Player.Get().transform, transform, followSpeed);
        FollowTargetAction followTargetAction = new FollowTargetAction(gameObject, Player.Get().gameObject.transform);

        Animator animator = GetComponent<Animator>();
        AnimatorSetSpeedAction setSpeedAction = new AnimatorSetSpeedAction(animator, rigidbody, "Speed", true);
        AnimatorSetTriggerAction setTrigger = new AnimatorSetTriggerAction(animator, "Run");
        
        state.SetUpMe(new StateAction[] { setVelocity, followTargetAction, setSpeedAction, setTrigger });
        return state;
    }

    protected State SetUpAttackState(){
        State state = new State();
        SetVelocity2DAction stopAction = new SetVelocity2DAction(GetComponent<Rigidbody>(), Vector3.zero, false);
        
        Animator animator = GetComponent<Animator>();
        AnimatorResetTriggerAction resetTrigger = new AnimatorResetTriggerAction(animator, "Run");
        AnimatorSetTriggerAction setTrigger = new AnimatorSetTriggerAction(animator, "Attack");
        
        state.SetUpMe(new StateAction[] { stopAction, resetTrigger, setTrigger });
        return state;
    }
    #endregion

    #region TransitionSetUp
    protected virtual Transition FollowToAttack(State follow, State attack) {
        Transition transition = new Transition();
        CheckDistanceCondition distanceCondition = new CheckDistanceCondition(transform, Player.Get().transform,
            distanceToStartAttack, COMPARISON.LESSEQUAL);
        transition.SetUpMe(follow, attack, new Condition[]{ distanceCondition });
        return transition;
    }
    protected virtual Transition AttackToFollow(State attack, State follow) {
        Transition transition = new Transition();
        CheckDistanceCondition distanceCondition = new CheckDistanceCondition(transform, Player.Get().transform,
            distanceToStopAttack, COMPARISON.GREATEREQUAL);
        transition.SetUpMe(attack, follow, new Condition[]{ distanceCondition });
        return transition;
    }
    
    #endregion
}