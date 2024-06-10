using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAnimationRedirector : MonoBehaviour
{
    [SerializeField]
    private AbilityBase throwAbility;
    
    public void EventRedirect(AnimationEvent animEvent)
    { 
        Debug.Log("Animation Event triggered: " + animEvent);
        if (throwAbility is IThrowAbility)
        {
            //TODO remove
            IThrowAbility ability = (IThrowAbility) throwAbility;
            ability.TriggerThrow();
        } else
        {
            Debug.LogWarning("Referenced throwAbility (" + throwAbility + ") does not implement IThrowAbility interface");
        }
    }
}
