using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Field)]
public class ConditionalHideAttribute : PropertyAttribute
{
    public string ConditionalSourceField;
    public string Header;  // ヘッダー文字列を追加
    public bool HideInInspector = false;

    public ConditionalHideAttribute(string conditionalSourceField, string header = "")
    {
        ConditionalSourceField = conditionalSourceField;
        Header = header;
    }
}