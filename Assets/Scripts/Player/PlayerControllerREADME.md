PlayerControllerの説明

IPlayerStateMachine（インターフェース）を実装しているクラスはListなどでまとめることができる

今回はDictionaryを使用しkeyを各々の型（クラス）としてvalueをIPlayerStateMachineとして管理している

なぜDictionaryを使うのかというとListより検索効率が良いから

\_currentStateを切り替えていくことで現在の状態に合わせた処理にしていくことができる

例

　Idle　->　待機（入力待ち）

　Run　->　移動処理　　　　　　　　　それぞれの状態にあった処理をするように切り替えていく

　Jump　->　ジャンプの処理



他の状態に切り替わった時はEnter()が呼ばれ、それ以降は毎フレームUpdate()が呼ばれ続ける、現在の状態から抜けるときにExit()が呼ばれる

