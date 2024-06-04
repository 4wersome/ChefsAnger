using System;

public enum DamageType
{
    Melee,
    Ranged
}

public interface IDamageble

{
    public void TakeDamage(DamageContainer damage);
}