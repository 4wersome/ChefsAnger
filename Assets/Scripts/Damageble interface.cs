using System;

public enum DamageType
{
    Melee,
    Ranged
}

public interface IDamageble

{
    
    public void TakeDamage(DamageType type, float amount);

}