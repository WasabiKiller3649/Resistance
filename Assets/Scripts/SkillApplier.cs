using UnityEngine;
using System;
public class SkillApplier : MonoBehaviour, IApplySkill
{
    //イベント参照
    [SerializeField]
    private SkillHolder[] _skillHolder;


    //適用するスキルの効果値
    private float _skillEffectValue;

    //Player弾のダメージを上げるスキルイベント
    public event Action<SkillApplier> OnApplyBulletDamageUPSkill;

    //PlayerMaxHPをあげるスキルイベント
    public event Action<SkillApplier> OnApplyPlayerMaxHPSkill;

    //Player弾の連射スキル
    public event Action<SkillApplier> OnApplyBulletMultiShotSkill;

    //弾の射撃間隔が短縮されるスキル
    public event Action<SkillApplier> OnApplyAddFireRateSkill;
    private void OnEnable()
    {
        for (int i = 0; i < _skillHolder.Length; i++)
        {
            _skillHolder[i].OnSkillSelect += ApplySkill;
        }
    }
    private void ApplySkill(SkillData skillDate)
    {
        //適用するスキルの効果値を取得
        _skillEffectValue = skillDate.GetEffectValue();
        switch (skillDate.GetSkillType())
        {
            case SkillType.Type.BulletDamage://Player弾ダメUP
                //適用される側に知らせる
                OnApplyBulletDamageUPSkill?.Invoke(this);
                break;
            case SkillType.Type.PlayerMaxHP:
                //適用される側に知らせる
                OnApplyPlayerMaxHPSkill?.Invoke(this);
                break;
            case SkillType.Type.BulletMultiShot:
                //適用される側に知らせる
                OnApplyBulletMultiShotSkill?.Invoke(this);
                break;
            case SkillType.Type.AddFireRate:
                //適用される側に知らせる
                OnApplyAddFireRateSkill?.Invoke(this);
                break;
        }
    }
    public float ApplySkill()
    {
        //スキルの効果値を返却
        return _skillEffectValue;
    }
}
