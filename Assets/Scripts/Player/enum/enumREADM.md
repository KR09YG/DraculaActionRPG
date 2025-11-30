C#のenum（列挙型）について

enumは関連する定数のグループを定義するための型です。コード内で「状態」や「種類」を表現する際に、数値や文字列の代わりに意味のある名前を使えるようにします。

基本的な使い方

csharp// 基本的な定義

public enum Direction

{

    North,   // 0

    East,    // 1

    South,   // 2

    West     // 3

}



// 使用例

Direction currentDirection = Direction.North;



if (currentDirection == Direction.North)

{

    Debug.Log("北を向いています");

}

値の指定

csharppublic enum WeaponType

{

    None = 0,

    Sword = 1,

    Bow = 2,

    Magic = 5,

    Hammer = 10

}

Unityでの実践的な例

1\. プレイヤーの状態管理

csharppublic enum PlayerState

{

    Idle,

    Walk,

    Run,

    Jump,

    Attack,

    Damage,

    Dead

}



public class PlayerController : MonoBehaviour

{

    private PlayerState currentState = PlayerState.Idle;

 

    void Update()

    {

        switch (currentState)

        {

            case PlayerState.Idle:

                HandleIdleState();

                break;

            case PlayerState.Walk:

                HandleWalkState();

                break;

            case PlayerState.Jump:

                HandleJumpState();

                break;

        }

    }

 

    public void ChangeState(PlayerState newState)

    {

        currentState = newState;

        Debug.Log($"State changed to: {newState}");

    }

}

2\. 敵の種類定義

csharppublic enum EnemyType

{

    Slime,

    Goblin,

    Dragon,

    Boss

}



public class Enemy : MonoBehaviour

{

    \[SerializeField] private EnemyType enemyType;

 

    void Start()

    {

        SetupEnemy(enemyType);

    }

 

    void SetupEnemy(EnemyType type)

    {

        switch (type)

        {

            case EnemyType.Slime:

                maxHP = 50;

                moveSpeed = 2f;

                break;

            case EnemyType.Dragon:

                maxHP = 500;

                moveSpeed = 5f;

                break;

        }

    }

}



3. インスペクターでの使用

csharppublic class WeaponData : MonoBehaviour

{

    \[SerializeField] private WeaponType weaponType;  // Inspectorでドロップダウン表示

    \[SerializeField] private DamageType damageTypes; // Flagsの場合は複数選択可能

}

便利な機能

string変換

csharpPlayerState state = PlayerState.Jump;

string stateName = state.ToString(); // "Jump"



// 逆変換

PlayerState parsed = (PlayerState)System.Enum.Parse(typeof(PlayerState), "Jump");

すべての値を取得

csharpforeach (PlayerState state in System.Enum.GetValues(typeof(PlayerState)))

{

    Debug.Log(state);

}

enumを使うメリット



可読性: if (state == 2) より if (state == PlayerState.Jump) の方が分かりやすい

安全性: タイポを防げる（コンパイルエラーになる）

保守性: 定義を一箇所で管理できる

IntelliSense: IDEで候補が表示される
※ AIのコピー



今回の場合

Nomalは通常状態、数値として0としても扱うことができる

Fireは火属性、数値として1としても扱うことができる　...etc

