using System;
using UnityEngine;

[Serializable]
public class PlayerIdle : IPlayerStateMachine
{
    private PlayerController _playerController;
    private const float MOVE_THRESHOLD = 0.1f;

    public PlayerIdle(PlayerController player, PlayerData playerData)
    {
        this._playerController = player;
    }

    /// <summary>
    /// この状態に入ったときに呼ばれる
    /// </summary>
    public void Enter()
    {
        Debug.Log("Idle状態に入りました");
    }

    /// <summary>
    /// この状態の時、毎フレーム呼ばれる
    /// </summary>
    public void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");

        // 移動入力が閾値以上ならWalk状態へ遷移
        if (Mathf.Abs(moveInput) > MOVE_THRESHOLD)
        {
            _playerController.ChangeState<PlayerRun>();
        }
    }

    /// <summary>
    /// この状態を抜けるときに呼ばれる
    /// </summary>
    public void Exit()
    {

    }

}
