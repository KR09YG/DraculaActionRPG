using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "Scriptable Objects/EnemySettings")]
public class EnemySettings : ScriptableObject
{
    public List<IEnemyMove> EnemyMoves = new List<IEnemyMove>();
}
