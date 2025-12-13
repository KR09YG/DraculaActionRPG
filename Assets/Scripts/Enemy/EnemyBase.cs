using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] EnemySettings _enemySettings;

    protected List<EnemyAction> _actions = new List<EnemyAction>();
    protected float _power;
    protected int _health;
    protected ElementType _elementTyp;

    protected int _currentActionIndex = 0;
    protected Sequence _currentSequence;

    private void Awake()
    {
        // 敵の基本設定を取得
        _power = _enemySettings.Power;
        _health = _enemySettings.Health;
        _elementTyp = _enemySettings.ElementTyp;
        _actions = _enemySettings.Actions;
        InitializeAction(_actions[_currentActionIndex].Action);
    }

    protected virtual void Start()
    {
        InitializeEnemy();
        ExecuteCurrentAction();
    }

    protected virtual void InitializeAction(EnemyActionBase action)
    {
        action.Initialize(transform.position);
        Debug.Log($"Initializing action: {action.GetType().Name}");
        Debug.Log($"Current position: {transform.position}");
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
        Debug.Log("Execute Action: " + _currentActionIndex);
        var currentAction = _actions[_currentActionIndex].Action;
        int loopCount = _actions[_currentActionIndex].LoopCount;
        float range = _actions[_currentActionIndex].Range;
        float speed = _actions[_currentActionIndex].Speed;
        currentAction.Execute(speed, range, transform, loopCount, OnActionComplete);
    }

    private void OnActionComplete()
    {
        _currentActionIndex = (_currentActionIndex + 1) % _actions.Count; // ループ処理
        InitializeAction(_actions[_currentActionIndex].Action);
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