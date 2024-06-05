using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThrowAbility 
{
    public void ThrowProjectile(Vector3 forward, Quaternion rotation);
}
