using UnityEngine;
public abstract class CasterStateBase
{
    //MonoBehaviourスクリプトが入っている
    protected CasterController movement;
    protected CasterStateBase(CasterController caster)
    {
        movement = caster;
    }


    //各Stateで時間を測る時に使う
    protected float elapsedTime;

    //行動開始時の座標を格納
    protected Vector3 firstPosition;
    public abstract void Enter();
    public abstract void Execute_Logic();
    public abstract void Exit();
}