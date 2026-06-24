using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
public class PlayerMove : MonoBehaviour
{
    //移動することを知らせる先
    [SerializeField]
    private PlayerController _playerController;
    //移動速度
    [SerializeField]
    private float _moveSpeed;

    [SerializeField]
    private HPController _hpController;
    //死んだとき、徐々にスピードが遅くなる
    private float _speedCorrection = 1;
    private WaitForSeconds _waitForDecrease = default;
    private WaitUntil _waitUntilDeath = default;
    private bool _isDeath = false;
    private readonly float _speedDecreaseTime = 1f;
    private readonly float _speedDecreaseRate = 0.9f;
    //これ以下になれば補正値を0にする 
    private readonly float _speedDecreaseEndVaue = 0.01f;
    private void Awake()
    {
        _waitForDecrease = new WaitForSeconds(_speedDecreaseTime);
        _waitUntilDeath = new WaitUntil(() => _isDeath == true);

        StartCoroutine(DecreaseSpeed());
    }
    private void OnEnable()
    {
        _hpController.OnDeath += OnDeath;
    }
    private void Update()
    {
        //上下移動
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        inputX = MoveSpeedX(inputX);
        inputY = MoveSpeedY(inputY);
        GiveMoveSpeed(inputX, inputY);
    }
    public float MoveSpeedX(float speedX)
    {
        speedX *= _moveSpeed;
        return speedX;
    }
    public float MoveSpeedY(float speedY)
    {
        speedY *= _moveSpeed;
        return speedY;
    }
    private void OnDeath()
    {
        _isDeath = true;
    }
    private IEnumerator DecreaseSpeed()
    {
        yield return _waitUntilDeath;
        _speedCorrection = 0.5f;
        while (_speedCorrection > _speedDecreaseEndVaue)
        {
            yield return _waitForDecrease;
            _speedCorrection -= Mathf.Lerp(_speedCorrection, 0, _speedDecreaseRate);
        }
        print("終わり");
        _speedCorrection = 0;
    }
    private void GiveMoveSpeed(float speedX, float speedY)
    {
        Vector2 v;
        v = new Vector2(speedX, speedY);
        if (v != null)
        {
            _playerController.SetMoveSpeed(v * _speedCorrection);
        }
    }
}
