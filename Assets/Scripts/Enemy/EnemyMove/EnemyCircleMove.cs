using System;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyCircleMove", menuName = "Enemy/Actions/CircleMove")]
public class EnemyCircleMove : EnemyActionBase
{
    [SerializeField] private bool _clockwise = true;
    [SerializeField] private float _startAngleOffset = 90f;

    private Vector2 _centerPosition;
    private Sequence _sequence;

    public override void Initialize(Vector2 position)
    {
        _centerPosition = position;

        if (_sequence != null && _sequence.IsActive())
        {
            _sequence.Kill();
            _sequence = null;
        }
    }

    public override void Execute(float speed, float range, Transform transform, int loopCount, Action onCompleted)
    {
        if (_sequence != null && _sequence.IsActive()) return;
        if (loopCount == 0)
        {
            Debug.LogError("ループ回数が0だとアクションは無意味です");
            return;
        }

        // 1周にかかる時間を計算（円周 / 速度）
        float circumference = 2f * Mathf.PI * range;
        float duration = circumference / speed;

        // 開始角度をラジアンに変換
        float startAngleRad = _startAngleOffset * Mathf.Deg2Rad;

        _sequence = DOTween.Sequence();

        // 円運動をTweenで実装
        _sequence.Append(DOTween.To(
            () => 0f,
            angle =>
            {
                float currentAngle = startAngleRad + angle * (_clockwise ? -1f : 1f);
                Vector2 newPos;
                newPos.x = _centerPosition.x + Mathf.Cos(currentAngle) * range;
                newPos.y = _centerPosition.y + Mathf.Sin(currentAngle) * range;
                transform.position = newPos;
            },
            Mathf.PI * 2f * loopCount,
            duration * loopCount
        ).SetEase(Ease.Linear));

        _sequence.OnComplete(() => onCompleted?.Invoke());
    }
}