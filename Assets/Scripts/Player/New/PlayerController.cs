using System;
using System.Collections;
using UnityEngine;
using XqLua;
using XqLua.Extension;

public class PlayerController : MonoBehaviour, IDisposable, IDamageable
{
    public IReactiveProperty<float> Health => _health;
    [Header("Combat")]
    [SerializeField] private PlayerCombatData _combatData = default;

    [Header("")]
    [SerializeField] private PlayerInputReceiver _inputReceiver = default;
    [SerializeField] private float _initialHealth = 0.0f;
    [SerializeField] private float _moveSpeed = 0.0f;
    [SerializeField] private float _decelerationFactor = 0.0f;
    [SerializeField] private float _decelerationTime = 0.0f;

    private PlayerCombat _combat = default;
    private ExPointContainer _exPointContainer = default;
    private ReactiveProperty<float> _health = default;
    private Disposables _disposables = default;
    private Coroutine _moveSpeedDeceleration = default;

    private float _moveSpeedFactor = 0.0f;

    private void Awake()
    {
        Initialize(new PlayerCombat(_combatData), new ExPointContainer());
    }

    public PlayerController Initialize(PlayerCombat combat, ExPointContainer exPointContainer)
    {
        _disposables = new Disposables();
        _health = new ReactiveProperty<float>(_initialHealth).AddTo(_disposables);

        _combat = combat;
        _exPointContainer = exPointContainer;

        _inputReceiver.Initialize();

        return this;
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        Vector2 moveInput = _inputReceiver.MoveValue;
        Move(moveInput, deltaTime);

        _combat.UpdateAttackInterval(deltaTime);
        if(_combat.TryAttack(transform.position, _inputReceiver.IsAttackPress))
        {
            if(_moveSpeedDeceleration != null)
            {
                StopCoroutine(_moveSpeedDeceleration);
            }

            _moveSpeedDeceleration = StartCoroutine(ResetMoveSpeedFactor(_decelerationTime));
        }
    }

    private void Move(Vector2 input, float deltaTime)
    {

        Vector2 moveValue = input * _moveSpeed * _moveSpeedFactor;

        transform.Translate(moveValue * deltaTime);
    }

    private IEnumerator ResetMoveSpeedFactor(float time)
    {
        _moveSpeedFactor = _decelerationFactor;

        yield return new WaitForSeconds(time);

        _moveSpeedFactor = 1.0f;

        _moveSpeedDeceleration = null;
    }

    public void TakeDamage(float damage)
    {
        _health.Value -= damage;
    }
}