using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventRedirector : MonoBehaviour
{
    public void EventRedirect(AnimationEvent animEvent)
    { 
        Debug.Log("Animation Event triggered: " + animEvent);
    }
}
