using System;
using System.Collections.Generic;
using UnityEngine;

public interface IDestroyable : IDamageble
{
    public void Destroy();
    public void DeSpawnItem();
}