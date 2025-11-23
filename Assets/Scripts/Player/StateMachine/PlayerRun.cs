using System;
using UnityEngine;

[Serializable]
public class PlayerRun : IPlayerStateMachine
{
    private PlayerController _playerController;
    private const float MOVE_THRESHOLD = 0.1f;
    private Rigidbody2D _rb;
    private PlayerData _playerData;
    public PlayerRun(PlayerController player, PlayerData playerData)
    {
        _playerController = player;
        _playerData = playerData;
    }

    /// <summary>
    /// この状態に入ったときに呼ばれる
    /// </summary>
    public void Enter()
    {

    }

    /// <summary>
    /// この状態の時、毎フレーム呼ばれる
    /// </summary>
    public void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveInput) < MOVE_THRESHOLD)
        {
            _playerController.ChangeState<PlayerIdle>();
        }

        // 水平方向の移動
        _playerController.MoveHorizontal(moveInput * _playerData.MoveSpeed);
    }

    /// <summary>
    /// この状態を抜けるときに呼ばれる
    /// </summary>
    public void Exit()
    {

    }
}
