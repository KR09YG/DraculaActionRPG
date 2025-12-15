using UnityEngine;

public struct DamageInfo
{
    public float BaseDamage;
    public ElementType AttackElement;
    public GameObject Attacker;
    public Vector2 HitPoint;
    public bool IsCritical;
}