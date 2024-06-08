using System;

public enum DamageType
{
    Melee,
    Ranged,
    Explosive
}

public interface IDamageble

{
    public void TakeDamage(DamageContainer damage);
}