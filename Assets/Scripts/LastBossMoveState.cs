using UnityEngine;
using System.Collections;
public class LastBossMoveState : LastBossStateBase
{
    //共通データを参照する
    private LastBossStateContext _context;
    //ドーナツ状の範囲からランダムな座標を取得する
    private RingPositionGenerator _ringPositionGenerator;

    //ドーナツの最大半径，最小半径
    private const float MAX_RADIUS = 7.5f;
    private const float MIN_RADIUS = 3f;

    //動く速さ
    private const float MOVE_SPEED = 6f;

    //取得した座標までの距離がこれ以下になれば，到着とみなす
    private const float MOVE_END_DISTANCE = 0.1f;
    //移動時間がこれ以上になると強制終了
    private const float MOVE_END_TIME = 4f;

    private bool _shouldMove = false;
    public LastBossMoveState(LastBossStateContext context)
    {
        _ringPositionGenerator = new RingPositionGenerator();
        _context = context;
    }
    public override void Enter()
    {
        _shouldMove = true;
        _context.boss.StartCoroutine(Move());
    }
    private IEnumerator Move()
    {
        Vector3 nextPos = default;
        Vector2 nextPositionVector = default;
        //移動先の座標を取得
        nextPos = _ringPositionGenerator.GetRandomPosition
            (MIN_RADIUS, MAX_RADIUS, _context.playerTransform.position);
        nextPositionVector = nextPos - _context.boss.transform.position;

        //取得したベクトル（方向）を正規化
        nextPositionVector = nextPositionVector.normalized;

        //取得した座標まで移動する
        _context.boss.Move(nextPositionVector, MOVE_SPEED);
        //走りアニメーション開始
        _context.animatorController.StartRun();
        float elapsedTime = 0;
        while (_shouldMove)
        {
            elapsedTime += Time.deltaTime;
            //座標まで到達したか判定
            if (Vector2.Distance(_context.boss.transform.position, nextPos) 
                <= MOVE_END_DISTANCE || elapsedTime > MOVE_END_TIME)
            {
                //State遷移
                Exit();
                _shouldMove = false;
            }
            yield return null;
        }
    }
    public void OnDeath()
    {
        _shouldMove = false;
    }

    public override void Exit()
    {
        //走りアニメーション終わり
        _context.animatorController.EndRun();

        _context.boss.Stop();
        _context.boss.ChangeState(LastBossStateChenger.LastBossState.Idle);
    }
}
