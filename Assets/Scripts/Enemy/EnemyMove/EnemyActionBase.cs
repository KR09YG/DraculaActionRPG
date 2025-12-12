using System;
using UnityEngine;

public abstract class EnemyActionBase : ScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private float _range;

    public float Speed => _speed;
    public float Range => _range;

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
