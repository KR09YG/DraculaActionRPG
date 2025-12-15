using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementMatchupTable", menuName = "Game/ElementMatchupTable")]
public class ElementMatchupTable : ScriptableObject
{
    [Tooltip("属性の順番（この順番で表が表示されます）")]
    public ElementType[] elements = new ElementType[]
    {
        ElementType.Fire,
        ElementType.Water,
        ElementType.Wind
    };

    [Tooltip("ダメージ倍率テーブル [攻撃側][防御側]")]
    [SerializeField]
    private float[] multipliers = new float[9]; // 3x3 = 9

    private const float DEFAULT_MULTIPLIER = 1.0f;

    /// <summary>
    /// 属性相性からダメージ倍率を取得
    /// </summary>
    public float GetMultiplier(ElementType attackElement, ElementType defenseElement)
    {
        int attackIndex = Array.IndexOf(elements, attackElement);
        int defenseIndex = Array.IndexOf(elements, defenseElement);

        int index = attackIndex * elements.Length + defenseIndex;

        if (index >= 0 && index < multipliers.Length)
        {
            return multipliers[index];
        }

        // 見つからなかった場合は1.0を返す
        return DEFAULT_MULTIPLIER;
    }

    /// <summary>
    /// ダメージ倍率を設定（エディタ用）
    /// </summary>
    public void SetMultiplier(int attackIndex, int defenseIndex, float value)
    {
        int index = attackIndex * elements.Length + defenseIndex;
        if (index >= 0 && index < multipliers.Length)
        {
            multipliers[index] = value;
        }
    }

    /// <summary>
    /// ダメージ倍率を取得（エディタ用）
    /// </summary>
    public float GetMultiplier(int attackIndex, int defenseIndex)
    {
        int index = attackIndex * elements.Length + defenseIndex;
        if (index >= 0 && index < multipliers.Length)
        {
            return multipliers[index];
        }
        return DEFAULT_MULTIPLIER;
    }

    /// <summary>
    /// 配列サイズを調整
    /// </summary>
    public void ResizeArray()
    {
        int newSize = elements.Length * elements.Length;
        if (multipliers.Length != newSize)
        {
            Array.Resize(ref multipliers, newSize);
        }
    }
}