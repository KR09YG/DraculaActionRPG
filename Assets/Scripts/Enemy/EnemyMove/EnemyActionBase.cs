using UnityEngine;

public abstract class EnemyActionBase : ScriptableObject
{
    [SerializeField] private float _duration;
    [SerializeField] private float _range;

    public float Duration => _duration;
    public float Range => _range;

    public abstract void Initialize(Vector2 position);
    public abstract void Execute(float speed, float duration, float range, Transform transform);
}
