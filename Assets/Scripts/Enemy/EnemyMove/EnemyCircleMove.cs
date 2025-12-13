using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyCircleMove", menuName = "Enemy/Actions/CircleMove")]
public class EnemyCircleMove : EnemyActionBase
{
    [SerializeField] private bool _clockwise = true;
    [SerializeField] private float _startAngleOffset = 90f;

    private float _time;
    private Vector2 _centerPosition;
    private Vector2 _tempVec;
    private bool _isCompleted;

    public override void Initialize(Vector2 position)
    {
        _centerPosition = position;
        _time = 0f;
        _isCompleted = false;
    }

    public override void Execute(float speed, float range, Transform transform, int loopCount, Action onCompleted)
    {
        if (_isCompleted) return;
        if (loopCount <= 0) Debug.LogError("ループ回数は1以上の値を指定してください。");

        // 時計回りなら負の値
        float direction = _clockwise ? -1f : 1f;
        _time += Time.deltaTime * speed * direction;

        if (loopCount > 0)
        {
            float totalRotation = Mathf.PI * 2f * loopCount;

            if (Mathf.Abs(_time) >= totalRotation)
            {
                _isCompleted = true;
                _time = totalRotation * direction;
            }
        }

        // 開始角度をラジアンに変換して加算
        float startAngleRad = _startAngleOffset * Mathf.Deg2Rad;
        float currentAngle = startAngleRad + _time;

        // 円運動の計算
        _tempVec.x = _centerPosition.x + Mathf.Cos(currentAngle) * range;
        _tempVec.y = _centerPosition.y + Mathf.Sin(currentAngle) * range;
        transform.position = _tempVec;
    }
}