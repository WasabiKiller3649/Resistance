using UnityEngine;

public abstract class LastBossStateBase
{

    //ŠeState‚ÌŒo‰ßŽžŠÔ‚ðŒv‘ª‚·‚é
    protected float elapsedTime = 0;
    public abstract void Enter();
    public abstract void Exit();
}
