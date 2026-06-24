using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BoneBulletGenerator : MonoBehaviour, IPoolConfig
{
    //弾を撃つイベントを参照する
    private List<BoneArcherController> _bones;

    //RequestHubを経由して骨弾を取得する
    [SerializeField]
    private ObjectPoolRequestHub _objectPoolRequestHub;

    //骨弾Poolの大きさ
    private uint _boneBulletPoolSize = default;

    //
    [SerializeField]
    private BoneArcherGenerator _boneArcherGenerator;

    //骨Poolが初期化完了したときのイベントを参照する
    [SerializeField]
    private ObjectPoolManager _boneArcherPool;

    //骨Poolが初期化完了したとき骨弾Poolを有効にする
    [SerializeField]
    private ObjectPoolManager _boneBulletPool;

    //撃つ弾の方向
    private readonly Vector2 _shootHorizontal = new Vector2(1, 0);
    private readonly Vector2 _shootVertical = new Vector2(0, 1);

    //撃つ弾のArcherから見た相対的な場所
    private readonly Vector3 _bulletPositionUp = new Vector3(0, 0.5f, 0);
    private readonly Vector3 _bulletPositionDown = new Vector3(0, -0.5f, 0);
    private readonly Vector3 _bulletPositionRight = new Vector3(0.5f, 0, 0);
    private readonly Vector3 _bulletPositionLeft = new Vector3(-0.5f, 0, 0);
    //
    private WaitForSeconds _waitForShot = default;
    private void Awake()
    {
        _bones = new List<BoneArcherController>();
    }
    private void OnEnable()
    {
        _boneArcherPool.OnPoolReady += EnableBoneBulletPoolManager;
    }

    private void ShotHub(int count, BoneArcherController archer)
    {
        StartCoroutine(Shot(count, archer));
    }
    private IEnumerator Shot(int count, BoneArcherController archer)
    {
        //残り射撃回数を消費
        count--;

        //ObjectPoolに弾丸を要求
        GameObject bullet1 = null;
        GameObject bullet2 = null;

        //オブジェクトぷ―るからの呼び出しとSetActive(true)はセットでやる
        bullet1 = _objectPoolRequestHub.RaiseOnGenerate();
        bullet1?.SetActive(true);

        bullet2 = _objectPoolRequestHub.RaiseOnGenerate();
        bullet2?.SetActive(true);

        //bullet1,2の位置を発射者の位置にする
        if (bullet1 != null && bullet2 != null)
        {
            BulletPositioning(bullet1, bullet2, archer);
        }
        yield return _waitForShot;
        if (count > 0)
        {
            StartCoroutine(Shot(count, archer));
        }
    }
    private void BulletPositioning(GameObject bullet1,GameObject bullet2, BoneArcherController archer)
    {
        //archerが縦横どちらに動いているかで弾の移動方向を変える
        if (archer.GetDirection().x == 0)
        {
            //縦に動いている時
            bullet1.transform.position = archer.transform.position + _bulletPositionUp;
            bullet2.transform.position = archer.transform.position + _bulletPositionDown;

            bullet1.GetComponent<BoneBulletMove>().MoveBullet(_shootHorizontal);
            bullet2.GetComponent<BoneBulletMove>().MoveBullet(_shootHorizontal * -1);
        }
        else
        {
            //横に動いているとき
            bullet1.transform.position = archer.transform.position + _bulletPositionRight;
            bullet2.transform.position = archer.transform.position + _bulletPositionLeft;

            bullet1.GetComponent<BoneBulletMove>().MoveBullet(_shootVertical);
            bullet2.GetComponent<BoneBulletMove>().MoveBullet(_shootVertical * -1);
        }
    }
    public void InitializeBones(BoneArcherController bone)
    {
        _bones.Add(bone);
        bone.OnShotBullet += ShotHub;
    }
    public void SetWaitForShot(float f)
    {
        //WaidForSecondsを毎回newしないようにする
        _waitForShot = new WaitForSeconds(f);
    }
    //BoneArcherPoolが初期化完了したとき、骨弾Poolを有効にする
    private void EnableBoneBulletPoolManager(GameObject archer)
    {
        //骨弾PoolのPoolSizeを設定する
        SetBoneBulletPoolSize(archer.GetComponent<BoneArcherController>());

        _boneBulletPool.enabled = true;
    }
    private void SetBoneBulletPoolSize(BoneArcherController archer)
    {
        _boneBulletPoolSize = (uint)
            (archer.GetBulletCount() * 
            _boneArcherGenerator.GetPoolSize() * archer.GetBulletNumber());
    }
    uint IPoolConfig.GetPoolSize()
    {
        //骨弓の数*（撃つ回数+1）*一度に撃つ数
        return _boneBulletPoolSize;
    }
}
