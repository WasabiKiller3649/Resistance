using UnityEngine;

public class LastBossStateController : LastBossStateBase
{
    //次にの行動パターンを決める
    private LastBossStateChenger _stateChenger = default;
    private LastBossStateChenger.LastBossState _currentState = default;
    private LastBossStateContext _context = default;

    public LastBossStateController(LastBossStateContext context)
    {
        _context = context;
        _stateChenger = new LastBossStateChenger();
        _currentState = LastBossStateChenger.LastBossState.Initialize;
    }
    public override void Enter()
    {
        //usedStateを復活させる
        _stateChenger.RemoveUsedState();
        //currentStateをusedStateに移動する

        _stateChenger.RemoveStandbyState(_currentState);

        //次のStateを抽選
        LastBossStateChenger.LastBossState next = _stateChenger.LotteryState();

        _currentState = next;
        _context.boss.ChangeState(next);
    }

    public override void Exit()
    {

    }
}
