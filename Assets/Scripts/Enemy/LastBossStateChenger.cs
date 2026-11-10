using UnityEngine;
using System.Collections.Generic;
public class LastBossStateChenger
{
    public enum LastBossState
    {
        Initialize,//初期化用
        Idle,
        Move,
        FunnelAttack,
        FunAttack,
        CaptureAttack,
    }

    //行動パターン抽選に使う配列
    private List<LastBossState> _standbyAttackStates = new List<LastBossState>()
    {
        LastBossState.FunAttack,
        LastBossState.FunnelAttack,
        LastBossState.CaptureAttack
    };
    //クールダウン中のStateを入れる
    private LastBossState _usedAttackState = default;

    //現在のStateをクールダウン中にする
    public void RemoveStandbyState(LastBossState current)
    {
        _standbyAttackStates.Remove(current);
        _usedAttackState = current;
    }
    //クールダウンが回復したStateをusedからstandbyへ
    public void RemoveUsedState()
    {
        if (_usedAttackState != LastBossState.Initialize)
        {
            _standbyAttackStates.Add(_usedAttackState);
            _usedAttackState = LastBossState.Initialize;
        }
    }
    //クールダウン中のStateをStandbyから消す
    //消した後のStandby配列から次の行動を抽選
    public LastBossState LotteryState()
    {
        int i = Random.Range(0, _standbyAttackStates.Count);
        LastBossState nextState = _standbyAttackStates[i];
        return nextState;
    }
}
