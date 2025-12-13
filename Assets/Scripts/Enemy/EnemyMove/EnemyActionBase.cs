using System;
using UnityEngine;

public abstract class EnemyActionBase : ScriptableObject
{
    /// <summary>
    /// ‰Šú‰»ˆ—
    /// </summary>
    /// <param name="position"></param>
    public abstract void Initialize(Vector2 position);
    /// <summary>
    /// Àsˆ—
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="duration"></param>
    /// <param name="range"></param>
    /// <param name="transform"></param>
    public abstract void Execute(float speed, float range, Transform transform, int loopCount, Action onCompleted = null);
}
