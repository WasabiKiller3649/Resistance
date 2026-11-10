using UnityEngine;
using System.Collections;
public class LastBossFunnelAttackState : LastBossStateBase
{
    //共通データを参照する
    private LastBossStateContext _context;

    //使う弾の数
    private const int BULLET_AMOUNT = 2;

    //Playerと弾の距離
    private const float RADIUS = 4;

    //武器が出現してから弾を撃つまでの時間
    private const float UNTIL_SHOT_TIME = 0.8f;
    private WaitForSeconds _waitForShot;

    //Playerを中心とした扇状の角度を取る
    private readonly float[] _angles = new float[BULLET_AMOUNT]
    {
        -220,
        220,
    };

    public LastBossFunnelAttackState(LastBossStateContext context)
    {
        _context = context;
        _waitForShot = new WaitForSeconds(UNTIL_SHOT_TIME);
    }
    public override void Enter()
    {
        _context.boss.StartCoroutine(StartAttack());
    }
    private IEnumerator StartAttack()
    {
        //武器が出現する、出現しきるまではPlayerを追尾する
        Vector2 center = _context.boss.transform.position;
        for (int i = 0; i < BULLET_AMOUNT; i++)
        {
            _context.weapons[i].SetActive(true);
        }
        bool _continue = true;
        while (_continue)
        {
            //この角度を中心とする
            float centerAngle = AngleCalculator.GetAngleToTarget(center,
                _context.playerTransform.position);
            Vector2 currentPlayerPosition = _context.playerTransform.position;
            for (int i = 0; i < BULLET_AMOUNT; i++)
            {
                GameObject weapon = _context.weapons[i];
                //武器の座標を扇状にする
                weapon.transform.position = _context.ringPositionGenerator.GetFunPosition(
                    RADIUS, _angles[i] + centerAngle, currentPlayerPosition);

                //武器の角度をPlayerに向ける
                weapon.transform.rotation = AngleCalculator.GetRotationToTarget(
                    weapon.transform.position, currentPlayerPosition,
                    LastBossStateContext.OFFSET_WEAPON_ANGLE);

                //出現アニメーション
                _context.weaponSpriteRenderers[i].color = //透明度を変更
                    _context.fadeController.FadeInAlpha(_context.weaponSpriteRenderers[i]);
                _context.weaponSpriteRenderers[i].color =//色を変更
                    _context.fadeController.FadeInColor(_context.weaponSpriteRenderers[i]);
            }

            if (_context.weaponSpriteRenderers[0].color.r >= LastBossStateContext.FADEIN_END_VALUE)
            {
                for (int i = 0; i < BULLET_AMOUNT; i++)
                {
                    _context.MaximizeColor(_context.weaponSpriteRenderers[i]);
                }
                _continue = false;
            }
            yield return null;
        }

        Vector2 playerPosition = _context.playerTransform.position;
        yield return _waitForShot;

        //弾を発射
        for (int i = 0; i < BULLET_AMOUNT; i++)
        {
            GameObject bullet = _context.objectPoolRequestHub.RaiseOnGenerate();
            if (bullet != null)
            {
                bullet.transform.position = _context.weapons[i].transform.position;

                bullet.transform.rotation = AngleCalculator.GetRotationToTarget(
                    bullet.transform.position, playerPosition,
                    LastBossStateContext.OFFSET_BULLET_ANGLE);
                if (bullet.TryGetComponent<IBulletStart>(out var bulletStart))
                {
                    bullet.SetActive(true);
                    bulletStart.Attack();
                }
            }
        }
        for (int i = 0; i < BULLET_AMOUNT; i++)
        {
            _context.weapons[i].SetActive(false);
        }
        Exit();
    }
    public override void Exit()
    {
        _context.boss.ChangeState(LastBossStateChenger.LastBossState.Move);
    }
}
