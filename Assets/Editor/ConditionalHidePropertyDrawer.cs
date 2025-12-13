using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ConditionalHideAttribute))]
public class ConditionalHidePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
        bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

        if (enabled)
        {
            Rect currentRect = position;

            // ヘッダーがある場合は上部に描画
            if (!string.IsNullOrEmpty(condHAtt.Header))
            {
                // ヘッダー用の矩形
                Rect headerRect = new Rect(
                    currentRect.x,
                    currentRect.y,
                    currentRect.width,
                    EditorGUIUtility.singleLineHeight
                );

                // ヘッダーを太字で描画
                EditorGUI.LabelField(headerRect, condHAtt.Header, EditorStyles.boldLabel);

                // フィールド用の矩形（ヘッダーの下）
                currentRect.y += EditorGUIUtility.singleLineHeight + 2;
                currentRect.height -= EditorGUIUtility.singleLineHeight + 2;
            }

            // 新しいGUIContentを作成（元の変数名を使用）
            GUIContent fieldLabel = new GUIContent(property.displayName, label.tooltip);

            // プロパティフィールドを描画
            EditorGUI.PropertyField(currentRect, property, fieldLabel, true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
        bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

        if (enabled)
        {
            // プロパティ本体の高さ
            float propertyHeight = EditorGUI.GetPropertyHeight(property, label, true);

            // ヘッダーがあればその分の高さを追加
            if (!string.IsNullOrEmpty(condHAtt.Header))
            {
                propertyHeight += EditorGUIUtility.singleLineHeight + 2;
            }

            return propertyHeight;
        }
        else
        {
            return -EditorGUIUtility.standardVerticalSpacing;
        }
    }

    private bool GetConditionalHideAttributeResult(ConditionalHideAttribute condHAtt, SerializedProperty property)
    {
        bool enabled = true;
        string propertyPath = property.propertyPath;
        string conditionPath = propertyPath.Replace(property.name, condHAtt.ConditionalSourceField);
        SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

        if (sourcePropertyValue != null)
        {
            enabled = sourcePropertyValue.boolValue;
        }
        else
        {
            Debug.LogWarning("条件となるフィールドが見つかりません: " + condHAtt.ConditionalSourceField);
        }

        return enabled;
    }
}