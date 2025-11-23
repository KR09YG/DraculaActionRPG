using System;
using UnityEngine;

[Serializable]
public class PlayerJump : IPlayerStateMachine
{
    private PlayerController _playerController;
    public PlayerJump(PlayerController player, PlayerData playerData)
    {
        this._playerController = player;
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

    }

    /// <summary>
    /// この状態を抜けるときに呼ばれる
    /// </summary>
    public void Exit()
    {

    }
}
