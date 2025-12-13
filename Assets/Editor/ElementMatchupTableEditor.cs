using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ElementMatchupTable))]
public class ElementMatchupTableEditor : Editor
{
    private ElementMatchupTable _table;
    private const float CELL_WIDTH = 60f;
    private const float CELL_HEIGHT = 20f;
    private const float LABEL_WIDTH = 80f;

    private void OnEnable()
    {
        _table = (ElementMatchupTable)target;
        _table.ResizeArray();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("属性相性テーブル", EditorStyles.boldLabel);
        EditorGUILayout.Space(5);

        EditorGUILayout.Space(10);

        // 属性配列の表示
        SerializedProperty elementsProp = serializedObject.FindProperty("elements");
        EditorGUILayout.PropertyField(elementsProp, true);

        if (GUILayout.Button("配列サイズを調整"))
        {
            _table.ResizeArray();
        }

        EditorGUILayout.Space(10);

        // テーブル描画
        DrawMatchupTable();

        EditorGUILayout.Space(10);    

        if (GUI.changed)
        {
            EditorUtility.SetDirty(_table);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawMatchupTable()
    {
        int elementCount = _table.elements.Length;
        if (elementCount == 0) return;

        EditorGUILayout.LabelField("ダメージ倍率表 [攻撃側 → 防御側]", EditorStyles.boldLabel);
        EditorGUILayout.Space(5);

        // 説明テキスト
        EditorGUILayout.HelpBox(
            "縦: 防御側の属性\n横: 攻撃側の属性\n例: 炎で水を攻撃 → 0.5倍ダメージ",
            MessageType.Info
        );
        EditorGUILayout.Space(5);

        // スクロールビュー開始
        EditorGUILayout.BeginVertical(GUI.skin.box);

        // ヘッダー行（防御側の属性）
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("", GUILayout.Width(LABEL_WIDTH)); // 左上の空白

        for (int i = 0; i < elementCount; i++)
        {
            GUI.backgroundColor = GetElementColor(_table.elements[i]);
            GUILayout.Label(_table.elements[i].ToString(), EditorStyles.boldLabel, GUILayout.Width(CELL_WIDTH));
            GUI.backgroundColor = Color.white;
        }
        EditorGUILayout.EndHorizontal();

        // データ行
        for (int attackIndex = 0; attackIndex < elementCount; attackIndex++)
        {
            EditorGUILayout.BeginHorizontal();

            // 行ヘッダー（攻撃側の属性）
            GUI.backgroundColor = GetElementColor(_table.elements[attackIndex]);
            GUILayout.Label(_table.elements[attackIndex].ToString(), EditorStyles.boldLabel, GUILayout.Width(LABEL_WIDTH));
            GUI.backgroundColor = Color.white;

            // セル（ダメージ倍率）
            for (int defenseIndex = 0; defenseIndex < elementCount; defenseIndex++)
            {
                float currentValue = _table.GetMultiplier(attackIndex, defenseIndex);

                // 色分け
                Color cellColor = GetMultiplierColor(currentValue);
                GUI.backgroundColor = cellColor;

                float newValue = EditorGUILayout.FloatField(currentValue, GUILayout.Width(CELL_WIDTH));

                if (newValue != currentValue)
                {
                    _table.SetMultiplier(attackIndex, defenseIndex, newValue);
                }

                GUI.backgroundColor = Color.white;
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();
    }

    private Color GetElementColor(ElementType element)
    {
        return element switch
        {
            ElementType.Fire => new Color(1f, 0.5f, 0.5f),   // 赤
            ElementType.Water => new Color(0.5f, 0.5f, 1f),  // 青
            ElementType.Wind => new Color(0.5f, 1f, 0.5f),  // 緑
            _ => Color.white
        };
    }

    private Color GetMultiplierColor(float multiplier)
    {
        if (multiplier >= 2.0f)
            return new Color(1f, 0.8f, 0.8f); // 薄い赤（有利）
        else if (multiplier <= 0.5f)
            return new Color(0.8f, 0.8f, 1f); // 薄い青（不利）
        else
            return Color.white; // 白（通常）
    }
}