using UnityEngine;
using System.Collections;
public class LastBossFunAttackState : LastBossStateBase
{
    //共通データを参照する
    private LastBossStateContext _context;
    //撃つ弾の数
    private const int BULLET_AMOUNT = 5;

    //武器出現から発射までのラグ秒
    private const float BULLET_FIRE_DELAY_TIME = 0.8f;
    private WaitForSeconds _waitForFire;

    //弾と弾の間隔の角度
    private readonly float[] _betweenBulletAngles =
        new float[BULLET_AMOUNT]
        {
            -40,
            -20,
            0,
            20,
            40,
        };

    //Stateが終了する時間
    private const float STATE_END_TIME = 0.8f;
    private WaitForSeconds _waitForStateEnd;

    //弾の並びが扇状になるように位置を補正する
    private const float WEAPON_RADIUS = 1;
    public LastBossFunAttackState(LastBossStateContext context)
    {
        _context = context;
        _waitForFire = new WaitForSeconds(BULLET_FIRE_DELAY_TIME);
        _waitForStateEnd = new WaitForSeconds(STATE_END_TIME);
    }
    public override void Enter()
    {
        for (int i = 0; i < BULLET_AMOUNT; i++)
        {
            _context.weapons[i].SetActive(true);
        }

        _context.boss.StartCoroutine(StartAttack());
    }
    private IEnumerator StartAttack()
    {
        //弾、武器に適用する回転量をとる
        Quaternion bulletToPlayerRotation = AngleCalculator.GetRotationToTarget(
            _context.boss.transform.position, _context.playerTransform.position, 
            LastBossStateContext.OFFSET_BULLET_ANGLE);
        Quaternion weaponToPlayerRotation = AngleCalculator.GetRotationToTarget(
            _context.boss.transform.position, _context.playerTransform.position,
            LastBossStateContext.OFFSET_WEAPON_ANGLE);

        Vector2 playerPosition = _context.playerTransform.position;
        Vector2 center = _context.boss.transform.position;

        //武器を扇状に並べる
        for (int i = 0; i < BULLET_AMOUNT; i++)
        {
            //
            float angle = _betweenBulletAngles[i] + AngleCalculator.GetAngleToTarget(
                _context.boss.transform.position, playerPosition);

            _context.weapons[i].transform.position = _context.ringPositionGenerator.GetFunPosition(
                WEAPON_RADIUS, angle, center);

            //武器の角度を放射状にする
            _context.weapons[i].transform.rotation = weaponToPlayerRotation *
                Quaternion.Euler(0, 0, _betweenBulletAngles[i]);

            _context.weapons[i].SetActive(true);
        }

        //武器を扇状にアニメーションさせる
        while (true)
        {
            //武器をフェードインさせる
            for (int i = 0; i < BULLET_AMOUNT; i++)
            {
                _context.weaponSpriteRenderers[i].color = //透明度を変更
                    _context.fadeController.FadeInAlpha(_context.weaponSpriteRenderers[i]);
                _context.weaponSpriteRenderers[i].color =//色を変更
                    _context.fadeController.FadeInColor(_context.weaponSpriteRenderers[i]);
            }

            if (_context.weaponSpriteRenderers[0].color.r > LastBossStateContext.FADEIN_END_VALUE)
            {
                break;
            }
            yield return null;
        }

        yield return _waitForFire;
        //武器のアニメーションが終わったらその位置から弾を発射する
        for (int i = 0; i < BULLET_AMOUNT; i++)
        {
            GameObject bullet = default;
            bullet = _context.objectPoolRequestHub.RaiseOnGenerate();

            if (bullet != null)
            {
                //弾を扇状に並べる
                float angle = _betweenBulletAngles[i] + AngleCalculator.GetAngleToTarget(
                    center, playerPosition);
                bullet.transform.position = _context.ringPositionGenerator.GetFunPosition(WEAPON_RADIUS,
                    angle, center);

                //弾の角度を扇状にする
                bullet.transform.rotation = bulletToPlayerRotation *
                    Quaternion.Euler(0, 0, _betweenBulletAngles[i]);
            }

            bullet.SetActive(true);
            Attack(bullet);
        }

        yield return _waitForStateEnd;

        //武器をフェードアウト
        while (true)
        {
            //武器をフェードインさせる
            for (int index = 0; index < BULLET_AMOUNT; index++)
            {
                //消失アニメーション
                _context.weaponSpriteRenderers[index].color = //透明度を変更
                    _context.fadeController.FadeOutAlpha(_context.weaponSpriteRenderers[index]);
                _context.weaponSpriteRenderers[index].color =//色を変更
                    _context.fadeController.FadeOutColor(_context.weaponSpriteRenderers[index]);
            }
            if (_context.weaponSpriteRenderers[0].color.a < LastBossStateContext.FADEOUT_END_VALUE)
            {
                for (int index = 0; index < BULLET_AMOUNT; index++)
                {
                    _context.MinimizeColor(_context.weaponSpriteRenderers[index]);
                }
                break;
            }
            yield return null;
        }
        Exit();
    }
    private void Attack(GameObject bullet)//引数の弾を発射する
    {
        if (bullet.TryGetComponent(out IBulletStart bulletStart))
        {
            bulletStart.Attack();
        }
    }

    public override void Exit()
    {
        for (int i = 0; i < _context.weapons.Length; i++)
        {
            _context.weapons[i].SetActive(false);
        }
        _context.boss.ChangeState(LastBossStateChenger.LastBossState.Move);
    }
}
