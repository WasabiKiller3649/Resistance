using UnityEngine;

public class EnemyHealthContainer : MonoBehaviour, IDamageable
{
    //初期最大HP
    [SerializeField]
    private int _initialMaxHealth;
    //ゲーム内で使用する最大HP
    private int _currentMaxHealth;

    //保有HP
    private float _container;

    //ゲージUI更新イベント
    [SerializeField]
    private UpdateHealthEventHub _updateHealthEventHub;
    private void Awake()
    {
        _currentMaxHealth = _initialMaxHealth;
    }
    private void OnEnable()
    {
        _container = _currentMaxHealth;
        //減った体力をゲージに反映させる
        _updateHealthEventHub.RaiseUpdateHealth(_currentMaxHealth, (int)_container);
    }
    public void TakeDamage(float damage)
    {
        _container -= damage;

        //減った体力をゲージに反映させる
        _updateHealthEventHub.RaiseUpdateHealth(_currentMaxHealth, (int)_container);
        if (_container <= 0)
        {
            if (gameObject.TryGetComponent<IBreakable>(out var breakable))
            {
                breakable.Destroy();
            }
        }
    }

}
