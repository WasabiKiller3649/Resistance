using UnityEngine;
using System;
public class PlayerController : MonoBehaviour
{
    //Rigidbodyを操作する
    [SerializeField]
    private PhisicsController _phisicsController;

    private Vector2 _moveSpeed = default;
    public Vector2 MoveSpeed
    {
        get { return _moveSpeed; }
    }
    private PlayerInput _playerInput;

    //入力を検知しているときを参照する
    [SerializeField]
    private PlayerBulletGenerator _playerBulletGenerator;

    private float _unMoveElapsedTime = 0;
    public float UnMoveElapsedTime
    {
        get { return _unMoveElapsedTime; }
    }
    public event Action OnStartMove;
    public event Action OnUnMove;

    [SerializeField]
    private HPController _hpController;
    [SerializeField]
    private SpriteRenderer _sprite;

    //弾を撃つ（入力をしている）時は移動速度を下げる
    private const float DECREASE_MOVR_SPEED = 0.5f;
    private void FixedUpdate()
    {
        _phisicsController.MoveSurface(_moveSpeed);
    }
    public void SubscribeShotSE(Action action)
    {
        _playerBulletGenerator.OnPlaySE += action;
    }
    public void SubscribeHitSE(Action action)
    {
        _hpController.OnPlaySE += action;
    }
    public void SetMoveSpeed(Vector2 speed)
    {
        if (_playerBulletGenerator.GetIsRapidfire())
        {
            //弾発射入力を受け付けているので移動速度を下げる
            _moveSpeed = speed * DECREASE_MOVR_SPEED;
        }
        else
        {
            //そうでないときは等速
            _moveSpeed = speed;
        }
    }
}