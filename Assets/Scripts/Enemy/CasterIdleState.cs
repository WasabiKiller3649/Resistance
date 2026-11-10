using UnityEngine;
using System.Collections;
public class CasterIdleState : CasterStateBase
{
    public CasterIdleState(CasterController caster) : base(caster)
    {
    }
    public override void Enter()
    {
        movement.StartCoroutine(Standby());
    }
    private IEnumerator Standby()
    {
        yield return new WaitForSeconds(movement.GetStandbyTime());
        Exit();
    }
    public override void Execute_Logic()
    {

    }
    public override void Exit()
    {
        movement.ChangeState();
    }
}
