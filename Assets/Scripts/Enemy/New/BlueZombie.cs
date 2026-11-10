using UnityEngine;
using XqLua;
using XqLua.Extension;

public class BlueZombie : MonoBehaviour
{
    public IReactiveProperty<float> Health => _health;

    [SerializeField] private BlueZombieParams _params = default;
    [SerializeField] private Transform _player = default;

    private ReactiveProperty<float> _health = default;
    private Disposables _disposables = default;

    public BlueZombie Initialize(BlueZombieParams param, Transform player)
    {
        _params = param;
        _player = player;

        _disposables = new Disposables();
        _health = new ReactiveProperty<float>(param.InitialeHealth).AddTo(_disposables);

        return this;
    }

    private void Move()
    {

    }

    private void Attack(IDamageable target)
    {

    }

    private void Death()
    {

    }
}