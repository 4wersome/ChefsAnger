using System;

public enum TypeOfDamage
{
    Melee,
    Ranged
}

public interface IDamageble

{
    
    public void TakeDamage(TypeOfDamage type, float amount);

}