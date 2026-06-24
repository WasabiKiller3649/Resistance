using UnityEngine;
using System.Collections;
public class CasterShootState : CasterStateBase
{
    public CasterShootState(CasterController caster) : base(caster)
    {
    }

    public override void Enter()
    {
        movement.StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        //Œ‚‚Á‚½‰ñ”
        int shotCount = 0;
        int bulletAmount = movement.GetBulletAmount();
        //‰Š’e‚ğŠi”[
        GameObject[] bullets = new GameObject[bulletAmount];

        for (shotCount = 0; shotCount < bulletAmount; shotCount++)
        {
            //‰Š’e‚ğæ“¾
            bullets[shotCount] = movement.RequestShootEvent();
            //À•W‚ğƒZƒbƒg
            bullets[shotCount].transform.position = movement.transform.position;

            //’eŠp“x‚ğPlayer‚Ö
            bullets[shotCount].transform.rotation = movement.RequestAngleToPlayer();

            if (shotCount > 0)
            {
                //‚Ì‚±‚è‚Ì’eŠp“x‚ğ‘O•ûîã‚Ì”ÍˆÍ‚©‚çƒ‰ƒ“ƒ_ƒ€‚ÅƒZƒbƒg
                bullets[shotCount].transform.rotation =
                    movement.RequestBulletRandomAngle() * movement.RequestAngleToPlayer();
            }

            bullets[shotCount].SetActive(true);

            yield return new WaitForSeconds(0.3f);
        }

        //State‘JˆÚII
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
