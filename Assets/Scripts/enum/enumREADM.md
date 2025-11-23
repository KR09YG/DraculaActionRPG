C#のenum（列挙型）について

enumは関連する定数のグループを定義するための型です。コード内で「状態」や「種類」を表現する際に、数値や文字列の代わりに意味のある名前を使えるようにします。

基本的な使い方

csharp// 基本的な定義

public enum Direction

{

&nbsp;   North,   // 0

&nbsp;   East,    // 1

&nbsp;   South,   // 2

&nbsp;   West     // 3

}



// 使用例

Direction currentDirection = Direction.North;



if (currentDirection == Direction.North)

{

&nbsp;   Debug.Log("北を向いています");

}

値の指定

csharppublic enum WeaponType

{

&nbsp;   None = 0,

&nbsp;   Sword = 1,

&nbsp;   Bow = 2,

&nbsp;   Magic = 5,

&nbsp;   Hammer = 10

}

Unityでの実践的な例

1\. プレイヤーの状態管理

csharppublic enum PlayerState

{

&nbsp;   Idle,

&nbsp;   Walk,

&nbsp;   Run,

&nbsp;   Jump,

&nbsp;   Attack,

&nbsp;   Damage,

&nbsp;   Dead

}



public class PlayerController : MonoBehaviour

{

&nbsp;   private PlayerState currentState = PlayerState.Idle;

&nbsp;   

&nbsp;   void Update()

&nbsp;   {

&nbsp;       switch (currentState)

&nbsp;       {

&nbsp;           case PlayerState.Idle:

&nbsp;               HandleIdleState();

&nbsp;               break;

&nbsp;           case PlayerState.Walk:

&nbsp;               HandleWalkState();

&nbsp;               break;

&nbsp;           case PlayerState.Jump:

&nbsp;               HandleJumpState();

&nbsp;               break;

&nbsp;       }

&nbsp;   }

&nbsp;   

&nbsp;   public void ChangeState(PlayerState newState)

&nbsp;   {

&nbsp;       currentState = newState;

&nbsp;       Debug.Log($"State changed to: {newState}");

&nbsp;   }

}

2\. 敵の種類定義

csharppublic enum EnemyType

{

&nbsp;   Slime,

&nbsp;   Goblin,

&nbsp;   Dragon,

&nbsp;   Boss

}



public class Enemy : MonoBehaviour

{

&nbsp;   \[SerializeField] private EnemyType enemyType;

&nbsp;   

&nbsp;   void Start()

&nbsp;   {

&nbsp;       SetupEnemy(enemyType);

&nbsp;   }

&nbsp;   

&nbsp;   void SetupEnemy(EnemyType type)

&nbsp;   {

&nbsp;       switch (type)

&nbsp;       {

&nbsp;           case EnemyType.Slime:

&nbsp;               maxHP = 50;

&nbsp;               moveSpeed = 2f;

&nbsp;               break;

&nbsp;           case EnemyType.Dragon:

&nbsp;               maxHP = 500;

&nbsp;               moveSpeed = 5f;

&nbsp;               break;

&nbsp;       }

&nbsp;   }

}



3. インスペクターでの使用

csharppublic class WeaponData : MonoBehaviour

{

&nbsp;   \[SerializeField] private WeaponType weaponType;  // Inspectorでドロップダウン表示

&nbsp;   \[SerializeField] private DamageType damageTypes; // Flagsの場合は複数選択可能

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

&nbsp;   Debug.Log(state);

}

enumを使うメリット



可読性: if (state == 2) より if (state == PlayerState.Jump) の方が分かりやすい

安全性: タイポを防げる（コンパイルエラーになる）

保守性: 定義を一箇所で管理できる

IntelliSense: IDEで候補が表示される
※ AIのコピー

