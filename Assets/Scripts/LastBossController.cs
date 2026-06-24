using UnityEngine;
using System;
public class LastBossController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidbody;

    //Playerの座標を参照
    [SerializeField]
    private Transform _playerTransform;

    //アニメーション制御
    [SerializeField]
    private LastBossAnimatorController _animatorController;

    //弾のオブジェクトプール
    [SerializeField]
    private ObjectPoolRequestHub _objectPoolRequestHub;

    //死スクリプト
    [SerializeField]
    private LastBossDestroy _lastBossDestroy;
    private bool _isDeath = false;
    public event Action OnDeath;
    #region　デバッグ用
    //ドーナツの最大半径
    [SerializeField]
    private float _maxRadius;
    //ドーナツの最小半径
    [SerializeField]
    private float _minRadius;

    #endregion

    //攻撃アニメーションで表示する武器
    [SerializeField]
    private GameObject[] _waepons;

    //各Stateを差し替える
    private LastBossStateBase _currentState;

    //各Stateと，Stateに渡す引数をまとめたコンテキスト
    private LastBossStateContext _stateContext;
    private LastBossStateController _stateController;
    private LastBossMoveState _moveState;
    private LastBossFunnelAttackState _funnelAttackState;
    private LastBossFunAttackState _funAttackState;
    private LastBossCaptureAttack _captureAttackState;

    private void Awake()
    {
        //State，コンテキスト初期化
        if (_waepons != null)
        {
            _stateContext = new LastBossStateContext(this, _playerTransform, _objectPoolRequestHub,
                _animatorController, _waepons);
        }
        _moveState = new LastBossMoveState(_stateContext);
        _funAttackState = new LastBossFunAttackState(_stateContext);
        _funnelAttackState = new LastBossFunnelAttackState(_stateContext);
        _captureAttackState = new LastBossCaptureAttack(_stateContext);
        _stateController = new LastBossStateController(_stateContext);

        _currentState = _stateController;
    }
    private void OnEnable()
    {
        _lastBossDestroy.OnDestroy += Death;
    }
    public void ChangeState(LastBossStateChenger.LastBossState next)
    {
        StopAllCoroutines();
        if (_isDeath)
        {
            return;
        }
        switch(next)
        {
            case LastBossStateChenger.LastBossState.Idle:
                _currentState = _stateController;
                break;
            case LastBossStateChenger.LastBossState.Move:
                _currentState = _moveState;
                break;
            case LastBossStateChenger.LastBossState.FunAttack:
                _currentState = _funAttackState;
                break;
            case LastBossStateChenger.LastBossState.FunnelAttack:
                _currentState = _funnelAttackState;
                break;
            case LastBossStateChenger.LastBossState.CaptureAttack:
                _currentState = _captureAttackState;
                break;
        }
        NextState();
    }
    private void Update()
    {
        print(_currentState);
    }
    public void Move(Vector2 vector, float speed)
    {
        _rigidbody.linearVelocity = vector * speed;
    }
    public void Stop()
    {
        _rigidbody.linearVelocity = Vector2.zero;
    }
    private void NextState()
    {
        _currentState.Enter();
    }
    private void Death()
    {
        OnDeath?.Invoke();
        Stop();
        _animatorController.Death();
        print(_moveState);
        _moveState.OnDeath();
        _isDeath = true;
        enabled = false;
    }

    public void Debu()
    {
        Death();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_playerTransform.position, _minRadius);
        Gizmos.DrawWireSphere(_playerTransform.position, _maxRadius);
    }
}
