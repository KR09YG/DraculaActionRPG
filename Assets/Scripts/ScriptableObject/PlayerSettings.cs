using UnityEngine;

[CreateAssetMenu(fileName = "PlayeStatusData", menuName = "Scriptable Objects/PlayeStatusData")]
public class PlayerSettings : ScriptableObject
{
    public int MaxHP;
    public int MaxMP;
    public float MoveSpeed;
    public float AttackCoolTime;
    
    //　設定する必要がない変数にHideInInspectorを付与することで、インスペクターに表示されなくしている
    [HideInInspector]　
    public int HP;
    [HideInInspector]
    public int MP;
    [HideInInspector]
    public PlayerAttribute PlayerAttribute = PlayerAttribute.Nomal;
}
