using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour {

    #region SerializedField
    //[SerializeField] private float patrolSpeed;
    [SerializeField] private float followSpeed;
    //[SerializeField] private Transform[] patrolStations;
    [SerializeField] private float distanceToStartAttack;
    [SerializeField] private float distanceToStopAttack;
    #endregion
    
    #region MonoBehaviour
    void Start() {
        StateMachine stateMachine = GetComponent<StateMachine>();
        
        //State patrol = SetUpPatrolState();
        State follow = SetUpBaseMovementState();
        State attack = SetUpAttackState();
        
        //patrol.SetUpMe(new Transition[] {});
        follow.SetUpMe(new Transition[] { FollowToAttack(follow, attack)});
        attack.SetUpMe(new Transition[] { AttackToFollow(attack, follow)});
        
        //stateMachine.Init(new State[]{patrol, follow, attack}, patrol);
        stateMachine.Init(new State[] { follow, attack }, follow);
    }
    #endregion

    #region StateSetUp
    /*protected virtual State SetUpPatrolState() {
        State state = new State();
        //SetVelocity3DAction setVelocity = new SetVelocity3DAction(GetComponent<Rigidbody>(), Player.Get().transform.position, transform.position, patrolSpeed);
        return state;
    }*/

    protected State SetUpBaseMovementState(){
        State state = new State();
        //SetVelocity3DAction setVelocity = new SetVelocity3DAction(GetComponent<Rigidbody>(), Player.Get().transform.position, transform.position, followSpeed);

        //FollowTargetAction followTargetAction = new FollowTargetAction(gameObject, Player.Get().gameObject);
        AnimatorSetTriggerAction setTrigger = new AnimatorSetTriggerAction(GetComponent<Animator>(), "Run");
        //state.SetUpMe(new StateAction[] { followTargetAction, setTrigger });
        return state;
    }

    protected State SetUpAttackState(){
        State state = new State();
        SetVelocity2DAction stopAction = new SetVelocity2DAction(GetComponent<Rigidbody>(), Vector3.zero, false);
        AnimatorResetTriggerAction resetTrigger = new AnimatorResetTriggerAction(GetComponent<Animator>(), "Run");
        AnimatorSetTriggerAction setTrigger = new AnimatorSetTriggerAction(GetComponent<Animator>(), "Attack");
        state.SetUpMe(new StateAction[] { stopAction, resetTrigger, setTrigger });
        return state;
    }
    #endregion

    #region TransitionSetUp
    protected virtual Transition FollowToAttack(State prev, State attack) {
        Transition transition = new Transition();
        /*CheckDistanceCondition distanceCondition = new CheckDistanceCondition(transform, Player.Get().transform,
            distanceToStartAttack, COMPARISON.LESSEQUAL);*/
        //transition.SetUpMe(prev, attack, new Condition[]{ distanceCondition });
        return transition;
    }
    protected virtual Transition AttackToFollow(State attack, State follow) {
        Transition transition = new Transition();
        /*CheckDistanceCondition distanceCondition = new CheckDistanceCondition(transform, Player.Get().transform,
            distanceToStopAttack, COMPARISON.GREATEREQUAL);*/
        //transition.SetUpMe(prev, attack, new Condition[]{ distanceCondition });

        return transition;
    }
    
    #endregion
}
