using DG.Tweening;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "EnemyVerticalMove", menuName = "Enemy/Actions/VerticalMove")]
public class EnemyVerticalMove : EnemyActionBase
{
    private Sequence _sequence;
    private float _startPosY;

    public override void Initialize(Vector2 position)
    {
        _startPosY = position.y;
        if (_sequence != null && _sequence.IsActive())
        {
            _sequence.Kill();
            _sequence = null;
        }
    }

    
    
    /// <param name="loopCount">-1なら無限ループする</param>
    public override void Execute(float speed, float range, Transform transform, int loopCount, Action onCompleted)
    {
        if (_sequence != null && _sequence.IsActive()) return;
        if (loopCount == 0) Debug.LogWarning("ループ回数が0だとアクションは無意味です");

        // 移動にかかる時間を速度から計算
        float moveDuration = range / speed;

        // 下が基準なので、そこから右にrangeの距離
        float upBound = _startPosY + range;

        _sequence = DOTween.Sequence();

        // 下から上
        _sequence.Append(transform.DOMoveY(upBound, moveDuration)
            .SetEase(Ease.InOutSine));

        // 上から下
        _sequence.Append(transform.DOMoveY(_startPosY, moveDuration)
            .SetEase(Ease.InOutSine));

        // 無限ループ（左端でループが始まるので自然
        _sequence.SetLoops(loopCount);
    }
}
