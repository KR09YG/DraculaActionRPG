using System;
using UnityEngine;

[Serializable]
public class PlayerData : MonoBehaviour
{
    public PlayerData(PlayerSettings playeSettings)
    {
        HP = playeSettings.HP;
        MaxHP = playeSettings.MaxHP;
        MP = playeSettings.MP;
        MaxMP = playeSettings.MaxMP;
        MoveSpeed = playeSettings.MoveSpeed;
        AttackCoolTime = playeSettings.AttackCoolTime;
        PlayerAttribute = playeSettings.PlayerAttribute;
    }

    public int HP;
    public int MaxHP;
    public int MP;
    public int MaxMP;
    public float MoveSpeed;
    public float AttackCoolTime;
    public PlayerAttribute PlayerAttribute;
}
