using UnityEngine;
using System;
using System.Collections;
public class ExPController : MonoBehaviour, IAttractable
{
    //出現時、放射状に広がるときの半径
    [SerializeField]
    private float _radius;


    //出現してから吸い込まれるまでの時間をカウントする
    private float _spawnElapsedTime = 0;

    //現在の状態
    private Action _currentState;

    //出現時の移動速度を補完する
    private const float INTER_POLATION_SPEED = 1;

    //Playerに検知されても一定時間吸い込まれないようするための時間
    private const float SUCTION_ENABLE_DELAY_TIME = 1;

    //目標地点までの距離がこれ以下になれば到着とみなす
    private const float ARRIVAL_DISTANCE = 0.3f;

    //出現時の移動目標の座標
    private Vector3 _radiusPosition = default;

    //Vector3だと実際の座標とずれるためTransformを参照
    private Transform _playerTransform = default;

    //Playerに吸い込まれる速度
    [SerializeField]
    private float _attractSpeed;

    private void OnEnable()
    {
        //自分を中心とした円の円周上の座標を取得
        _radiusPosition = UnityEngine.Random.insideUnitCircle.normalized * _radius;
        _radiusPosition += transform.position;

        //currentMovementをMoveRadiallyへ変更
        _currentState = MoveToRadiusPosition;
    }
    private void OnDisable()
    {
        _spawnElapsedTime = 0;
    }
    private void Update()
    {
        //現在の動きを実行
        _currentState?.Invoke();
    }
    private void MoveToRadiusPosition()//敵オブジェクトが倒れた時に実行される
    {
        //円周上の座標へ移動
        transform.position = Vector2.Lerp(transform.position, _radiusPosition, INTER_POLATION_SPEED * Time.deltaTime);

        //出現した時間をカウントする
        _spawnElapsedTime += Time.deltaTime;

        //円周上にたどり着いたら，移動解除
        if (Vector3.Distance(transform.position, _radiusPosition) < ARRIVAL_DISTANCE)
        {
            _currentState = null;
        }
    }

    private void MoveToPlayer()//Playerに吸い込まれる動き
    {
        transform.position = Vector3.MoveTowards
            (transform.position, _playerTransform.position, Time.deltaTime * _attractSpeed);
    }
    private IEnumerator WaitAttract(Transform target)//Playerに吸い込まれる
    {
        //出現してから一定時間は吸い込まれない
        yield return new WaitUntil(() => _spawnElapsedTime >= SUCTION_ENABLE_DELAY_TIME);

        //_playerTransform（目標地点）をPlayerから渡されたtargetにする
        _playerTransform = target;

        //CurrentMovementをMoveToPlayerにする
        _currentState = MoveToPlayer;
    }
    public void StartAttract(Transform target)//インターフェースメソッド
    {
        //Playerへ吸引開始
        StartCoroutine(WaitAttract(target));
    }
}
