using UnityEngine;
using System;
using System.Collections;
public class PlayerBulletGenerator : MonoBehaviour, IPoolConfig
{
    //プールオブジェクトのスクリプト
    [SerializeField]
    private ObjectPoolManager _objectPoolManager;

    //入力を読む
    [SerializeField]
    private PlayerInput _playerInput;

    //弾に渡すダメージデータ
    [SerializeField]
    private DamageManager _playerBulletDamage;

    //発射間隔
    [SerializeField]
    private float _shootInterval;

    private float _elapsedShootTime = 0;//弾を撃ってからの経過時間
    private bool _canShoot = false;//弾を撃てますよ
    private bool _shouldShoot = false;//弾を撃ちなさいよ
    private bool _isShooting = false;//弾を撃っていますよ
    private bool _isRapidfire = false;//連射している/Playerの移動速度にかかわる
    private WaitUntil _waitUntilCanShoot;


    //弾の出現位置
    [SerializeField]
    private GameObject _spawnPoint;

    //作るプールの大きさ
    [SerializeField]
    private uint _poolSize;

    //弾発射イベント
    [SerializeField]
    private ObjectPoolRequestHub _objectPoolRequestHub;

    //スキル獲得イベントを参照する
    [SerializeField]
    private SkillApplier _skillApplier;

    //一度に撃つ弾の量
    private int _requestBulletAmount = 1;

    //弾を撃つ標的を取得するイベント
    public event Func<Collider2D> OnRequestTarget;

    public event Action OnPlaySE;

    //弾丸の射撃間隔の最低時間，これ以上の速度では弾を連射できない
    private const float MIN_SHOOT_INTERVAL = 0.1f;

    //弾連射時，次に発射するまで少し待つ
    private WaitForSeconds _waitForNextShoot = new WaitForSeconds(0.1f);
    private void Awake()
    {
        _waitUntilCanShoot = new WaitUntil(() => _canShoot);//撃てるまで待ちますよ
    }
    private void OnEnable()
    {
        _objectPoolManager.OnCreatePool += PassDamageManager;
        _skillApplier.OnApplyBulletMultiShotSkill += ApplyMultiSkill;
        _skillApplier.OnApplyAddFireRateSkill += ApplyAddFireRateSkill;

        _playerInput.OnStartShot += StartShooting;
        _playerInput.OnEndShot += EndShooting;
    }
    private void Update()
    {
        if (_isShooting)//撃っている間
        {
            _elapsedShootTime += Time.deltaTime;
            if (_elapsedShootTime >= _shootInterval)
            {
                _elapsedShootTime = 0;//初期化
                _canShoot = true;//撃てますよ
            }
        }
    }
    private IEnumerator ShootBullet()
    {
        while (_shouldShoot)
        {
            for (int i = 1; i <= _requestBulletAmount; i++)
            {
                //連射開始
                _isRapidfire = true;
                //Poolへ発射要求
                GameObject bullet = _objectPoolRequestHub.RaiseOnGenerate();
                if (bullet != null)
                {
                    //弾の位置を_spawnPointの位置に上書き
                    bullet.transform.position = _spawnPoint.transform.position;

                    //弾を撃つ標的を取得
                    Collider2D target = OnRequestTarget();

                    if (target != null)
                    {
                        //弾の角度調整
                        Quaternion angle =
                            AngleCalculator.GetRotationToTarget(transform.position, target.transform.position, -90);
                        bullet.transform.rotation = angle;
                    }

                    //弾実体化
                    bullet.SetActive(true);
                    OnPlaySE?.Invoke();

                    _canShoot = false;//一度撃ったのでfalse
                }
                yield return _waitForNextShoot;
            }
            //連射終わり
            _isRapidfire = false;
            yield return _waitUntilCanShoot;//撃てるまで待ちますよ
        }

        _isShooting = false;//弾を撃っていませんよ
    }
    private void PassDamageManager(GameObject bullet)//弾にダメージ情報の参照を渡す
    {
        bullet.GetComponent<PlayerDamageServer>().SetDamageManager(_playerBulletDamage);
    }
    private void StartShooting()
    {
        _shouldShoot = true;//弾を撃ちなさいよ
        //弾を撃たせる指示
        if (!_isShooting)
        {
            _isShooting = true;//弾を撃っていますよ→true

            StartCoroutine(ShootBullet());//弾を撃つ
        }
    }
    #region スキル獲得
    private void ApplyMultiSkill(SkillApplier applier)//スキル適用
    {
        //スキルの効果値を取得し，メソッドへ渡す
        if (applier.TryGetComponent<IApplySkill>(out var applySkill))
        {
            AddRequestBulletAmount((int)applySkill.ApplySkill());
        }
    }
    private void ApplyAddFireRateSkill(SkillApplier applier)
    {
        //スキルの効果値を取得し，メソッドへ渡す
        if (applier.TryGetComponent<IApplySkill>(out var applySkill))
        {
            ReduceShootInterval(applySkill.ApplySkill());
        }
    }
    private void AddRequestBulletAmount(int amount)
    {
        //渡された効果値分，一度に撃つ弾の量を渡す
        _requestBulletAmount += amount;
    }
    private void ReduceShootInterval(float value)
    {
        if (_shootInterval > MIN_SHOOT_INTERVAL)
        {
            _shootInterval -= value;
        }
    }
    #endregion
    private void EndShooting()
    {
        //弾撃ち終了指示
        _shouldShoot = false;//撃ち方やめ
    }
    public bool GetIsRapidfire()
    {
        return _isRapidfire;
    }
    public uint GetPoolSize()
    {
        return _poolSize;
    }
}
