using UnityEngine;
using XqLua;
using XqLua.Extension;

public class BlueZombie : MonoBehaviour, IDamageable
{
    public IReactiveProperty<float> Health => _health;

    [SerializeField] private BlueZombieParams _params = default;
    [SerializeField] private Transform _player = default;

    private ReactiveProperty<float> _health = default;
    private Disposables _disposables = default;
    private bool _wasActivated = false;

    public BlueZombie Initialize(BlueZombieParams param, Transform player)
    {
        _params = param;
        _player = player;

        _disposables = new Disposables();
        _health = new ReactiveProperty<float>(param.InitialeHealth).AddTo(_disposables);

        return this;
    }

    private void Update()
    {
        if (!_wasActivated)
        {
            return;
        }

        float deltaTime = Time.deltaTime;

        Move(deltaTime);
    }

    private void Move(float deltaTime)
    {
        Vector2 targetPos = Vector2.MoveTowards(transform.position, _player.position, _params.MoveSpeed * deltaTime);

        transform.position = targetPos;
    }

    private void Attack(IDamageable target)
    {

    }

    /// <summary>
    /// èoåªââèo
    /// </summary>
    public void Appearance()
    {
        _wasActivated = true;
    }

    private void Death()
    {

    }

    public void TakeDamage(float damage)
    {
        _health.Value -= damage;
    }
}