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
            IThrowAbility test = (IThrowAbility) throwAbility;
            test.ThrowProjectile(new Vector3(5,0,5), Quaternion.identity);
        }
    }
}
