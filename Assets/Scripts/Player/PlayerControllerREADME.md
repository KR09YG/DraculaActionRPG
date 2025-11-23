PlayerControllerの説明

IPlayerStateMachineを継承しているクラスはListなどでまとめることができる

今回はDictionaryを使用しkeyを各々の型（クラス）としてvalueをIPlayerStateMachineとしている

\_currentStateを切り替えていくことで現在の状態に合わせた処理にしていくことができる

例

　Idle　->　特に何もしない

　Run　->　移動処理　　　　　　　　　それぞれの状態にあった処理をするように切り替えていく

　Jump　->　ジャンプの処理

