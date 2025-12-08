using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Enemy1 : MonoBehaviour
{
    [SerializeField] private List<EnemyActionBase> _enemyActions;
    [Header("縦揺れを入れる場合はチェックを入れてください")]
    [SerializeField] private bool _isHorizontalMove;
    [SerializeField]
    [ConditionalHide("_isHorizontalMove", "EnemyのVerticalMoveを設定してください")]
    private EnemyActionBase _enemyVerticalMove;
    [SerializeField]
    [ConditionalHide("_isHorizontalMove")]
    private float _verticalMoveSpeed = 0.5f;
    [SerializeField] private float _speed = 0.5f;
    private float _range;
    private float _duration;

    private void Awake()
    {
        _range = _enemyActions[0].Range;
        _duration = _enemyActions[0].Duration;
        _enemyActions[0].Initialize(transform.position);
        if (_isHorizontalMove)
        {
            _enemyVerticalMove.Initialize(transform.position);
            Debug.Log(_enemyVerticalMove);
        }
    }

    private void Update()
    {
        _enemyActions[0].Execute(_speed, _duration, _range, transform);

        if (_isHorizontalMove)
        {
            float duration = _enemyVerticalMove.Duration;
            float range = _enemyVerticalMove.Range;
            _enemyVerticalMove.Execute(_verticalMoveSpeed, duration,  range, transform);
        }
    }
}
