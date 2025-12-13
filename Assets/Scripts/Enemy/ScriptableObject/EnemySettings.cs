using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "Scriptable Objects/EnemySettings")]
public class EnemySettings : ScriptableObject
{
    [Header("Action Settings")]
    public List<EnemyAction> Actions = new List<EnemyAction>();
    public int Health = 3;
    public float Power;
    public ElementType ElementTyp;
}

