using System;
using UnityEngine;

public class SkillHolder : MonoBehaviour
{
    //SkillApplierにスキルデータを渡す
    public event Action<SkillData> OnSkillSelect;


    //スキルアイコン
    [SerializeField]
    private SkillIconSetter _skillIconSetter;

    //LevelUPManagerからSkillを受け取る
    public SkillData _skillData { get; private set; }
    public void SelectSkill()//SkillApplierに渡す
    {
        OnSkillSelect?.Invoke(_skillData);
    }
    public void SetSkillData(SkillData setSkill)
    {
        _skillData = setSkill;

        //アイコン変更などの処理
        _skillIconSetter.SetSkillIcon(_skillData.GetUiIcon());
    }
}
