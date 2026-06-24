using UnityEngine;

public class CasterMoveState : CasterStateBase
{
    public CasterMoveState(CasterController caster) : base(caster)
    {
    }
    Vector3 nextPosition;
    Vector3 startPosition;
    public override void Enter()
    {
        //insideUnitCircleは半径1の円からランダムな円周の座標を取る
        //normalizedはベクトルをそのままに，長さを1にする
        //それに半径をかけて伸ばす
        nextPosition = Random.insideUnitCircle.normalized * movement.GetRadius();
        startPosition = movement.transform.position;
    }

    public override void Execute_Logic()
    {
        movement.transform.Translate(nextPosition * Time.deltaTime);
        if (Vector3.Distance(movement.transform.position, startPosition + nextPosition) < 0.1f)
        {
            //State遷移！！！
            Exit();
        }
    }

    public override void Exit()
    {
        movement.ChangeState();
    }
}
