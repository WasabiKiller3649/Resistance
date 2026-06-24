using UnityEngine;
using System;
public class HPController : MonoBehaviour, IDamageable, IHealable
{
    #region　スキル獲得関連
    //スキル適用イベント参照
    [SerializeField]
    private SkillApplier _skillAplier;

    //自分が受け取るスキルのタイプ
    [SerializeField]
    private SkillType.Type _skillType;
    #endregion
    //Playerの最大HP
    [SerializeField]
    private int _maxHealth = 100;

    //HPの値を格納する
    private HealthContainer _healtContainer;

    //現在HPと最大HPをUIに渡す
    [SerializeField]
    private UpdateHealthEventHub _updateHealthEventHub;

    public event Action OnPlaySE;

    //死
    public event Action OnDeath = default;
    private void Awake()
    {
        _healtContainer = new HealthContainer(_maxHealth);
    }
    private void OnEnable()
    {
        _skillAplier.OnApplyPlayerMaxHPSkill += ApplySkill;
    }
    public void TakeDamage(float damage)
    {
        _healtContainer.TakeDamage(-damage);
        if (_healtContainer.GetCurrentHealth() <= 0)
        {
            OnDeath?.Invoke();
        }

        //UIを更新する
        _updateHealthEventHub.RaiseUpdateHealth
            (_maxHealth, Mathf.Clamp(_healtContainer.GetCurrentHealth(), 0, _maxHealth));

        OnPlaySE?.Invoke();
    }

    public void Heal(int amount)
    {
        _healtContainer.TakeHeal(amount);

        //UIを更新する
        _updateHealthEventHub.RaiseUpdateHealth
            (_maxHealth, _healtContainer.GetCurrentHealth());
    }
    #region　スキル獲得

    private void ApplySkill(SkillApplier applier)//スキル適用
    {
        //IApplySkillを取得してApplySkillを実行
        if (applier.TryGetComponent<IApplySkill>(out var applySkill))
        {
            //スキルの効果値を取得し，最大HP値に適用
            float skillvalue = applySkill.ApplySkill();

            _maxHealth += (int)skillvalue;

            //スキル適用後，現在のHPを最大まで回復
            Heal(_maxHealth - _healtContainer.GetCurrentHealth());
        }
    }
    #endregion
}
