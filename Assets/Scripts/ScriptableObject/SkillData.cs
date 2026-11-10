using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/SkillDate")]
public class SkillData : ScriptableObject
{
    //スキルタイプ
    [SerializeField]
    private SkillType.Type _skillType;

    //効果値
    [SerializeField]
    private float _effectValue;

    //スキル取得画面で表示するアイコン
    [SerializeField]
    private Sprite _uiIcon;

    //スキル取得画面に表示するスキルの効果説明文
    [SerializeField]
    private string _skillEffectDescription;
    public SkillType.Type GetSkillType()
    {
        return _skillType;
    }
    public float GetEffectValue()
    {
        return _effectValue;
    }
    public Sprite GetUiIcon()
    {
        return _uiIcon;
    }
    public string GetSkillEffectDescription()
    {
        return _skillEffectDescription;
    }
}
