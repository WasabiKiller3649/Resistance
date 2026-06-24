using UnityEngine;

public class DamageManager : MonoBehaviour
{
    //初期ダメージ参照
    [SerializeField]
    private DamageData _damageDate;

    #region　スキル獲得関連
    //スキル適用イベント参照
    [SerializeField]
    private SkillApplier _skillAplier;
    #endregion

    //現在のダメ―ジ
    private float _currentDamageValue;
    private void OnEnable()
    {
        //イベント購読
        _skillAplier.OnApplyBulletDamageUPSkill += ApplySkill;
    }
    private void OnDisable()
    {
        //イベント購読解除
        _skillAplier.OnApplyBulletDamageUPSkill -= ApplySkill;
    }
    private void Awake()
    {
        //初期値設定
        AddDamageValue(_damageDate.GetDamage());
    }
    #region　スキル獲得

    private void ApplySkill(SkillApplier applier)//スキル適用
    {
        //IApplySkillを取得してApplySkillを実行
        if (applier.TryGetComponent<IApplySkill>(out var applySkill))
        {
            AddDamageValue(applySkill.ApplySkill());
        }
    }
    #endregion
    private void AddDamageValue(float changeValue)
    {
        _currentDamageValue += changeValue;
    }
    public float GetCurrentDamageValue()
    {
        return _currentDamageValue;
    }
}
