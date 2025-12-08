using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyHorizontalMove", menuName = "Enemy/Actions/HorizontalMove")]
public class EnemyHorizontalMove : EnemyActionBase
{
    private Sequence _sequence;
    private float _startPosX;

    public override void Initialize(Vector2 position)
    {
        _startPosX = position.x;
        if (_sequence != null && _sequence.IsActive())
        {
            _sequence.Kill();
            _sequence = null;
        }
    }

    public override void Execute(float speed, float duration, float range, Transform transform,int loopCount)
    {
        if (_sequence != null && _sequence.IsActive()) return;

        // 移動にかかる時間を速度から計算
        float moveDuration = range / speed;

        float rightBound = _startPosX + range;
        Debug.Log(range);

        _sequence = DOTween.Sequence();

        // 左端 → 右端
        _sequence.Append(transform.DOMoveX(rightBound, moveDuration)
            .SetEase(Ease.InOutSine));

        // 右端 → 左端
        _sequence.Append(transform.DOMoveX(_startPosX, moveDuration)
            .SetEase(Ease.InOutSine));

        _sequence.SetLoops(loopCount);
    }
}