using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    private Rigidbody playerRigidBody;

    [SerializeField]
    public PlayerAnimatorMngr AnimatorMgnr;


    private AbilityBase[] abilities;

    private void Awake()
    {
        abilities = GetComponentsInChildren<AbilityBase>();

        foreach (AbilityBase ability in abilities)
        {
            ability.Init(this);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetForward(Vector3 forward)
    {
        playerRigidBody.transform.forward = forward;
    }
    public void SetVelocity(float X, float Z)
    {
        Vector3 velocity = playerRigidBody.velocity;
        velocity.z = Z;
        velocity.x = X;
        playerRigidBody.velocity = velocity;
    }

    public void SetRotation(Quaternion rot)
    {
        player.transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 10f);

    }
}
