using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerSettings _playerSettings;
    [SerializeField] private LayerMask _groundLayer;
    private PlayerData _playerData;

    // 移動に関するステートマシン
    private IPlayerStateMachine _currentState;
    private Dictionary<Type, IPlayerStateMachine> _states;

    private const float GROUND_CHECK_DISTANCE = 0.6f;
    private Vector3 _tempVec;

    private void Awake()
    {
        if (_playerSettings == null)
        {
            Debug.LogError("PlayerSettingsが設定されていません！");
            return;
        }

        if (!TryGetComponent<Collider2D>(out Collider2D collider2D))
        {
            Debug.LogError("Collider2Dコンポーネントがアタッチされていません！");
        }

        // プレイヤーデータの初期化
        _playerData = new PlayerData(_playerSettings);

        // ステートマシンの初期化
        _states = new Dictionary<Type, IPlayerStateMachine>
        {
            { typeof(PlayerIdle), new PlayerIdle(this, _playerData)},
            { typeof(PlayerRun), new PlayerRun(this, _playerData)},
            { typeof(PlayerJump), new PlayerJump(this, _playerData)}
        };

        // 初期ステートの設定(最初はIdleとする)
        _currentState = _states[typeof(PlayerIdle)];
        _currentState.Enter();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log(IsGrounded());
        }
        _currentState?.Update();
    }

    /// <summary>
    /// 水平方向の移動
    /// </summary>
    public void MoveHorizontal(float speed)
    {
        _tempVec.x = speed * Time.deltaTime;
        _tempVec.y = 0;
        _tempVec.z = 0;
        transform.position += _tempVec;
    }

    /// <summary>
    /// ステートの変更
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void ChangeState<T>() where T : IPlayerStateMachine
    {
        Type stateType = typeof(T);

        if (_states.TryGetValue(stateType, out IPlayerStateMachine state))
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
        }
        else
        {
            Debug.LogError($"{stateType.Name}が存在しません！");
        }
    }

    /// <summary>
    /// 接地判定
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        return Physics2D.Raycast(
            transform.position, Vector2.down, GROUND_CHECK_DISTANCE, _groundLayer
            );
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="damage">プレイヤーに与えるダメージ量</param>
    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            Debug.LogWarning("ダメージは正の値を指定してください");
            return;
        }

        _playerData.HP -= damage;

        // ここにダメージを受けた際の処理


        if (_playerData.HP <= 0)
        {
            Debug.Log("Playerは死亡しました。");
            // 死亡処理をここに追加
            _playerData.HP = 0;
        }
    }

    public void Heal(int heal)
    {
        if (heal < 0)
        {
            Debug.LogWarning("回復量は正の値を指定してください");
            return;
        }

        _playerData.HP += heal;
        // 最大HPを超えないようにする
        if (_playerData.HP > _playerSettings.MaxHP)
        {
            _playerData.HP = _playerSettings.MaxHP;
        }

        //　ここに回復時の処理

    }
}
