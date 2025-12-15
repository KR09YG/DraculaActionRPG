using UnityEngine;

public interface IDamageable
{
    void TakeDamage(DamageInfo damageInfo);
    void HitStop();
    ElementType GetElementType();
    bool IsAlive();
}
