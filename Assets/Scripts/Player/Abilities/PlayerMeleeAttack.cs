using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : AbilityBase
{
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
       
       if (InputManager.Player.LaunchAbility.IsPressed())
        {
           
            playerController.AnimatorMgnr.SetTriggerParameter("Attack");
        }
    }

    private void Attack()
    {
      
    }
}
