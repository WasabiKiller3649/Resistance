using UnityEngine;
using System;
using System.Collections;

public class CasterController : MonoBehaviour
{
    //ŠeState‚ª“ü‚é
    private CasterStateBase[] _currentStates;
    private int _statesNum = 0;
    private int _stateCount = 3;

    //Player‚ğ“ü‚ê‚é
    [SerializeField]
    private Transform _playerPosition;

    //IdleState‚ÌŠÔ
    [SerializeField]
    private float _standbyTime;

    //‰~üˆÚ“®‚Ì”¼Œa
    [SerializeField]
    private float _radius;

    [SerializeField]
    private LineRenderer _lineRenderer;


    //UŒ‚ü‚ğ•\¦‚·‚éŠÔ
    [SerializeField]
    private float _shootNoticeTime;

    //UŒ‚ü‚ÌÅ‰‚Ì‘¾‚³
    [SerializeField]
    private float _noticeLineStartWidth;

    //‰Š’e‚ğŒ‚‚Âƒ‰ƒ“ƒ_ƒ€‚ÈŠp“x
    [SerializeField]
    private float _randomBulletShotAngle;

    //‰Š’e‚ÌŠp“x‚ğì‚é
    public event Func<float, Quaternion> OnRequestBulletRandomAngle;

    //ˆê“x‚É‘Å‚Â‰Š’e‚Ì”
    [SerializeField]
    private int _bulletAmount;
    //’e‚ğ”­Ë‚·‚éƒCƒxƒ“ƒg
    public event Func<GameObject> OnShootBullet;

    //HPContainer
    [SerializeField]
    private CasterHealthContainer _healthContainer;

    //€‚Ê‚Æ‚«‚É•úo‚·‚éŒoŒ±’l‚Ì”
    [SerializeField]
    private int _smallExPAmount;

    //‚Å‚©‚¢‚Ù‚¤‚ÌŒoŒ±’l‚Ì”
    [SerializeField]
    private int _lergeExPAmount;

    private void Awake()
    {

        _currentStates = new CasterStateBase[4];
        _currentStates[0] = new CasterIdleState(this);
        _currentStates[1] = new CasterMoveState(this);
        _currentStates[2] = new CasterShootNoticeState(this, _lineRenderer);
        _currentStates[3] = new CasterShootState(this);
    }
    private void OnEnable()
    {
        _currentStates[_statesNum].Enter();
        _healthContainer.OnDead += Dead;
    }
    // Update is called once per frame
    private void Update()
    {
        _currentStates[_statesNum].Execute_Logic();
    }
    private void Dead()
    {
        //€‚Êˆ—
        DeathEventHub.RaiseDeath(transform.position, _smallExPAmount, _lergeExPAmount);
        gameObject.SetActive(false);
    }
    public float GetRadius()
    {
        return _radius;
    }
    public Vector3 GetPlayerPosition()
    {
        return _playerPosition.position;
    }
    public float GetStandbyTime()
    {
        return _standbyTime;
    }
    public float GetNoticLineWidth()
    {
        return _noticeLineStartWidth;
    }
public GameObject RequestShootEvent()
    {
        return OnShootBullet?.Invoke();
    }
    public Quaternion RequestAngleToPlayer()
    {
        return AngleCalculator.GetRotationToTarget(_playerPosition.position, transform.position);
    }
    public Quaternion RequestBulletRandomAngle()
    {
        return OnRequestBulletRandomAngle.Invoke(_randomBulletShotAngle);
    }
    public int GetBulletAmount()
    {
        return _bulletAmount;
    }
    public float GetShootNoticeTime()
    {
        return _shootNoticeTime;
    }
    public void ChangeState()
    {
        _statesNum++;
        if (_statesNum > _stateCount)
        {
            _statesNum = 0;
        }
        _currentStates[_statesNum].Enter();
    }
}