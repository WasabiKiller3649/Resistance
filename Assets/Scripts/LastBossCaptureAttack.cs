using UnityEngine;
using System.Collections;
public class LastBossCaptureAttack : LastBossStateBase
{
    private const int BULLET_AMOUNT = 6;
    private readonly LastBossStateContext _context;
    private Vector2 _playerPosition = default;
    //武器が表示される間隔
    private const float WEAPON_APPEARANCE_TIME = 0.1f;
    private readonly WaitForSeconds _waitForAppearance;

    private bool _isReady = false;
    private WaitUntil _waitUntilReady = default;

    //武器が表示されてから弾が発射されるまでの時間
    private const float FIRE_LATE_TIME = 0.5f;
    private readonly WaitForSeconds _waitForFire;
    //武器同士の間の角度
    private readonly float[] _betweenWeaponAngles = new float[BULLET_AMOUNT]
    {
        -10,
        10,
        -20,
        20,
        -30,
        30
    };
    public LastBossCaptureAttack(LastBossStateContext context)
    {
        _context = context;
        _waitForAppearance = new WaitForSeconds(WEAPON_APPEARANCE_TIME);
        _waitForFire = new WaitForSeconds(FIRE_LATE_TIME);
        _waitUntilReady = new WaitUntil(() => _isReady == true);
    }
    public override void Enter()
    {
        _context.boss.StartCoroutine(StartAttack());
    }
    private IEnumerator StartAttack()
    {
        _playerPosition = _context.playerTransform.position;
        float centerAngle = AngleCalculator.GetAngleToTarget(_playerPosition,
            _context.boss.transform.position);

        for (int i = 0; i < BULLET_AMOUNT; i++)
        {
            _context.weapons[i].SetActive(true);
        }
        int count = 0;
        //武器をPlayerを中心とした扇状に並べる、前の二つから順に表示していく
        for (int i = 0; i < BULLET_AMOUNT; i++)
        {
            count++;
            GameObject weapon = _context.weapons[i];

            //Playerを中心とした扇形に武器を並べる
            float radius = Vector3.Distance(_context.boss.transform.position, _playerPosition);
            weapon.transform.position = SetPosition(radius,
                centerAngle + _betweenWeaponAngles[i], _playerPosition);

            //武器をPlayerに向ける
            Quaternion rotation = AngleCalculator.GetRotationToTarget(
                weapon.transform.position, _playerPosition,
                LastBossStateContext.OFFSET_WEAPON_ANGLE);
            weapon.transform.rotation = rotation;

            _context.boss.StartCoroutine(FadeInWeapon(i));
            if (count == 2)//武器を二つ表示したら少し待つ
            {
                yield return _waitForAppearance;

                count = 0;
            }
        }

        yield return _waitUntilReady;

        for (int i = 0; i < BULLET_AMOUNT; i += 2)
        {
            yield return _waitForAppearance;//消えるときも出現時と同じ時間待つ
            _context.boss.StartCoroutine(FadeOutWeapon(i));
            _context.boss.StartCoroutine(FadeOutWeapon(i + 1));
        }
        Exit();
    }
    private IEnumerator FadeOutWeapon(int index)
    {
        _isReady = false;
        while (true)
        {
            //消失アニメーション
            _context.weaponSpriteRenderers[index].color = //透明度を変更
                _context.fadeController.FadeOutAlpha(_context.weaponSpriteRenderers[index]);
            _context.weaponSpriteRenderers[index].color =//色を変更
                _context.fadeController.FadeOutColor(_context.weaponSpriteRenderers[index]);
            if (_context.weaponSpriteRenderers[index].color.a < LastBossStateContext.FADEOUT_END_VALUE)
            {
                _context.MinimizeColor(_context.weaponSpriteRenderers[index]);
                break;
            }
            yield return null;
        }
        _isReady = true;
    }
    private Vector2 SetPosition(float radius, float angle, Vector2 center)
    {
        return _context.ringPositionGenerator.GetFunPosition(radius, angle, center);
    }
    private IEnumerator FadeInWeapon(int index)
    {
        _isReady = false;
        while (true)
        {
            //出現アニメーション
            _context.weaponSpriteRenderers[index].color = //透明度を変更
                _context.fadeController.FadeInAlpha(_context.weaponSpriteRenderers[index]);
            _context.weaponSpriteRenderers[index].color =//色を変更
                _context.fadeController.FadeInColor(_context.weaponSpriteRenderers[index]);

            if (_context.weaponSpriteRenderers[index].color.a > LastBossStateContext.FADEIN_END_VALUE)
            {
                _context.MaximizeColor(_context.weaponSpriteRenderers[index]);
                break;
            }
            yield return null;
        }

        //武器が表示されたら少し待って弾を発射する
        yield return _waitForFire;

        Fire(index);
        _isReady = true;
    }
    private void Fire(int index)
    {
        GameObject bullet = _context.objectPoolRequestHub.RaiseOnGenerate();
        if (bullet != null)
        {
            bullet.transform.position = _context.weapons[index].transform.position;

            //弾をPlayerに向ける
            Quaternion rotation = AngleCalculator.GetRotationToTarget(
                _context.weapons[index].transform.position,
                _playerPosition, LastBossStateContext.OFFSET_BULLET_ANGLE);
            bullet.transform.rotation = rotation;

            bullet.SetActive(true);
            if (bullet.TryGetComponent<IBulletStart>(out var start))
            {
                start.Attack();
            }
        }
    }

    public override void Exit()
    {
        for (int i = 0; i < BULLET_AMOUNT; i++)
        {
            _context.weapons[i].SetActive(false);
        }
        _context.boss.ChangeState(LastBossStateChenger.LastBossState.Move);
    }
}
