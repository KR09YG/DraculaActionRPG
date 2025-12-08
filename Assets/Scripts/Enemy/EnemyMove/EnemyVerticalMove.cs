using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyVerticalMove", menuName = "Enemy/Actions/VerticalMove")]
public class EnemyVerticalMove : EnemyActionBase
{
    private Sequence _sequence;
    private Vector2 _startPos;

    public override void Initialize(Vector2 position)
    {
        _startPos = position;
        if (_sequence != null && _sequence.IsActive())
        {
            _sequence.Kill();
            _sequence = null;
        }
    }

    public override void Execute(float speed, float duration, float range, Transform transform)
    {
        if (_sequence != null && _sequence.IsActive()) return;

        // 移動にかかる時間を速度から計算
        float moveDuration = range / speed;

        // 下が基準なので、そこから右にrangeの距離
        float upBound = _startPos.x + range;

        _sequence = DOTween.Sequence();

        // 下から上
        _sequence.Append(transform.DOMoveX(upBound, moveDuration)
            .SetEase(Ease.InOutSine));

        // 上から下
        _sequence.Append(transform.DOMoveX(_startPos.x, moveDuration)
            .SetEase(Ease.InOutSine));

        // 無限ループ（左端でループが始まるので自然
        _sequence.SetLoops(-1);
    }
}
