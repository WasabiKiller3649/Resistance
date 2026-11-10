using UnityEngine;
using System.Collections;
public class CasterShootNoticeState : CasterStateBase
{
    LineRenderer _lineRenderer;
    public CasterShootNoticeState(CasterController caster, LineRenderer lineRenderer) : base(caster)
    {
        _lineRenderer = lineRenderer;
    }
    public override void Enter()
    {
        //線を引く始点を設定
        SetLineRendererPositions(0, movement.transform.position);

        //コルーチンでState遷移
        movement.StartCoroutine(ChangeState());

        //ラインレンダラーの線の太さリセット
        _lineRenderer.SetWidth(movement.GetNoticLineWidth(), movement.GetNoticLineWidth());
    }

    public override void Execute_Logic()
    {
        //線の終点をPlayerの位置に設定
        SetLineRendererPositions(1, movement.GetPlayerPosition());

        //線を徐々に細くする
        SetLineRendererWidth(-Time.deltaTime / movement.GetShootNoticeTime());
    }
    private void SetLineRendererWidth(float value)
    {
        _lineRenderer.SetWidth(_lineRenderer.startWidth + value,
            _lineRenderer.endWidth + value);
    }
    private void SetLineRendererPositions(int index, Vector3 position)
    {
        //ラインレンダラーの超点数を増やしてからじゃないとエラーになる
        if (_lineRenderer.positionCount <= index)
        {
            _lineRenderer.positionCount++;
        }
        _lineRenderer.SetPosition(index, position);
    }
    private void DeleteLine()
    {
        //ラインレンダラーの線を消す
        _lineRenderer.positionCount = 0;
    }
    private IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(movement.GetShootNoticeTime());

        //State遷移！！！
        Exit();
    }
    public override void Exit()
    {
        DeleteLine();
        movement.ChangeState();
    }
}
