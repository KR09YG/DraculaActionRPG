using UnityEngine;

public class FlyEnemy : EnemyBase
{
    [Header("縦揺れをする場合はチェックをいれる")]
    [SerializeField] private bool _isShaking;
    [ConditionalHide("_isShaking"),SerializeField] private EnemyVerticalMove _verticalMove;
    [ConditionalHide("_isShaking"), SerializeField] private float _speed;
    [ConditionalHide("_isShaking"), SerializeField] private float _shakeRange;

    protected override void Start()
    {
        base.Start(); // 親クラスのStartを先に実行

        if (_isShaking)
        {
            InitializeAction(_verticalMove);
            VerticalShaking();
        }
    }

    protected override void InitializeAction(EnemyActionBase action)
    {
        base.InitializeAction(action);
    }

    public override void TakeDamage(float damage, ElementType type)
    {

    }

    protected override void Attack()
    {

    }

    protected override void InitializeEnemy()
    {
        
    }

    protected override void OnDeath()
    {

    } 

    private void VerticalShaking()
    {
        // 無限ループなので-1を指定、終了コールバックは不要
        _verticalMove.Execute(_speed, _shakeRange, transform, -1, null);
    }
}
