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
            // ヘッダーがある場合は描画
            if (!string.IsNullOrEmpty(condHAtt.Header))
            {
                // ヘッダーを描画
                Rect headerRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.LabelField(headerRect, condHAtt.Header, EditorStyles.boldLabel);

                // フィールドをHeaderの下に描画（変数名はlabelをそのまま使用）
                Rect fieldRect = new Rect(
                    position.x,
                    position.y + EditorGUIUtility.singleLineHeight + 2,
                    position.width,
                    EditorGUI.GetPropertyHeight(property, label, true)
                );
                EditorGUI.PropertyField(fieldRect, property, label, true);  // labelをそのまま渡す
            }
            else
            {
                // ヘッダーがない場合は通常通り描画
                EditorGUI.PropertyField(position, property, label, true);
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
        bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

        if (enabled)
        {
            float height = EditorGUI.GetPropertyHeight(property, label, true);

            // ヘッダーがあればその分の高さを追加
            if (!string.IsNullOrEmpty(condHAtt.Header))
            {
                height += EditorGUIUtility.singleLineHeight + 2;
            }

            return height;
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