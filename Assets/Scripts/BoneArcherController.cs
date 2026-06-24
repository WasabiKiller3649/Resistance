using UnityEngine;
using System;
using System.Collections;
public class BoneArcherController : MonoBehaviour
{
    //移動速度
    [SerializeField]
    private float _moveSpeed;

    //移動する方向
    private Vector2 _direction = default;

    //動く、撃つなどのState
    private enum MoveState
    {
        Move,
        DirectionChange,
        Stop,
    }
    private MoveState _state = MoveState.Stop;

    //Stateを切り替える時間
    [SerializeField]
    private float _moveStateTime;
    [SerializeField]
    private float _directionChangeTime;

    //移動方向を変えるイベント
    public Func<Vector2, Vector2> OnRequestDirection;

    //移動方向を変える
    [SerializeField]
    private BoneMoveDirectionChange _directionChange;

    #region  弾関連
    //弾を撃つ回数
    private const int BULLET_COUNT = 3;

    //一度に弾を撃つ数（上下、左右で一度に二回）
    private const int BULLET_NUMBER = 2;

    //弾を撃つイベント
    public Action<int, BoneArcherController> OnShotBullet;
    #endregion

    //Rigidbodyを触る
    [SerializeField]
    private PhisicsController _phisicsController;

    //Destroy
    [SerializeField]
    private EnemyDestroy _destroy;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //配列の初期化
        _directionChange.SetMoveVector(_moveSpeed);

        //壁に当たった時反射する
        _phisicsController.OnReflection += Reflection;

        //移動方向の初期化
        _direction = OnRequestDirection(_direction);
    }
    private IEnumerator ChangeState(float time)
    {
        //3秒待機
        yield return new WaitForSeconds(time);

        switch (_state)
        {
            case MoveState.Move:
                _phisicsController.Stop();
                //State変え
                _state = MoveState.DirectionChange;
                StartCoroutine(ChangeState(_directionChangeTime));
                break;
            case MoveState.DirectionChange:
                //移動方向かえる
                _direction = OnRequestDirection(_direction);

                //現在の移動方向へ移動
                _phisicsController.MoveSurface(_direction);
                //State変え
                _state = MoveState.Move;

                //Stateコルーチン再開
                StartCoroutine(ChangeState(_moveStateTime));

                //nullじゃなければ弾を撃て
                OnShotBullet?.Invoke(BULLET_COUNT, this);
                break;
        }
    }
    private void Reflection(string s)
    {
        if (s == "Border")
        {
            _phisicsController.MoveSurface(_direction * -1);
        }
    }
    public int GetBulletCount()
    {
        return BULLET_COUNT;
    }
    public int GetBulletNumber()
    {
        return BULLET_NUMBER;
    }
    public float GetBulletInterval()
    {
        float f = _moveStateTime / BULLET_COUNT;
        return f;
    }
    public Vector2 GetDirection()
    {
        return _direction;
    }
    public void ReMove()
    {
        _state = MoveState.Move;
        _phisicsController.MoveSurface(_direction);
        OnShotBullet?.Invoke(BULLET_COUNT, this);
        StartCoroutine(ChangeState(_moveStateTime));
    }
}
