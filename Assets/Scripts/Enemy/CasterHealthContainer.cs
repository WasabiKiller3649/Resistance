using UnityEngine;
using System;
public class CasterHealthContainer : MonoBehaviour
{
    //CasterのHP
    [SerializeField]
    private int _maxHealth;
    private int _health;
    [SerializeField]
    private CasterHealthCounter _counter;

    //ゲージの表示を更新するイベント
    [SerializeField]
    private UpdateHealthEventHub _updateHealthEventHub;

    //HPが0になったとき
    public event Action OnDead;
    private void OnEnable()
    {
        _health = _maxHealth;
        _counter.OnTakeDamage += TakeDamage;
    }
    private void OnDisable()
    {
        _counter.OnTakeDamage -= TakeDamage;
    }
    private void TakeDamage(float damage)
    {
        _health -= (int)damage;
        print("Casterが" + damage + "受けた");

        //減った分の表示をゲージに反映する
        _updateHealthEventHub.RaiseUpdateHealth(_maxHealth, _health);
        if (_health <= 0.005f)
        {
            OnDead();
        }
    }
}
