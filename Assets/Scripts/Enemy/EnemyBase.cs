using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] EnemySettings _enemySettings;

    private List<EnemyAction> _actions = new List<EnemyAction>();
    private float _speed = 2f;
    private float _actionRange = 3f;
    private float _power;
    private int _health;
    private ElementType _elementTyp;

    private int _currentActionIndex = 0;
    private Sequence _currentSequence;

    private void Awake()
    {
        // 敵の基本設定を取得
        _speed = _enemySettings.Speed;
        _actionRange = _enemySettings.ActionRange;
        _power = _enemySettings.Power;
        _health = _enemySettings.Health;
        _elementTyp = _enemySettings.ElementTyp;
        _actions = _enemySettings.Actions;
    }

    private void Start()
    {
        InitializeEnemy();
        ExecuteCurrentAction();
    }

    /// <summary>
    /// 敵固有の初期化処理（継承先で必ず実装）
    /// </summary>
    protected abstract void InitializeEnemy();

    /// <summary>
    /// ダメージ処理（継承先で必ず実装）
    /// </summary>
    /// <param name="type">ダメージを与える側の属性</param>
    public abstract void TakeDamage(float damage, ElementType type);

    /// <summary>
    /// 死亡処理（継承先で必ず実装）
    /// </summary>
    protected abstract void OnDeath();

    /// <summary>
    /// 攻撃処理（継承先で必ず実装）
    /// </summary>
    protected abstract void Attack();
    
    private void ExecuteCurrentAction()
    {
        if (_enemySettings.Actions.Count == 0) return;

        var currentAction = _actions[_currentActionIndex].Action;
        int loopCount = _actions[_currentActionIndex].LoopCount;
        currentAction.Execute(_speed, _actionRange, transform, loopCount, OnActionComplete);
    }

    private void OnActionComplete()
    {
        _currentActionIndex = (_currentActionIndex + 1) % _actions.Count; // ループ処理
        ExecuteCurrentAction();
    }

    private void OnDestroy()
    {
        // Sequenceのクリーンアップ
        if (_currentSequence != null && _currentSequence.IsActive())
        {
            _currentSequence.Kill();
        }
    }
}