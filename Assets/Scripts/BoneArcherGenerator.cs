using UnityEngine;
using System;
using System.Collections;
public class BoneArcherGenerator : MonoBehaviour, IPoolConfig
{
    //BoneArcherを生成する間隔
    [SerializeField]
    private float _generateTime;
    private WaitForSeconds _waitForGenerate = default;

    //オブジェクトプールを入れる
    [SerializeField]
    private ObjectPoolManager _objectPoolManager;

    //骨生成イベント
    [SerializeField]
    private ObjectPoolRequestHub _obujectPoolRequestHub;

    //BoneArcherの弾Generater
    [SerializeField]
    private BoneBulletGenerator _boneBulletGenerator;

    //生成するプールの大きさ
    [SerializeField]
    private uint _poolSize;

    private bool _shouldSpawn = false;

    //骨が出現する位置を取得するイベント
    public event Func<Vector2> OnRequestSpawnPosition;

    //フェーズ進行じのスポーン間隔短縮
    private float _spwanRateIncreaseValue = 0.5f;
    private void OnEnable()
    {
        _objectPoolManager.OnCreatePool += PassBoneArcher;//プール初期化処理
        _objectPoolManager.OnPoolReady += PassBulletInterval;
    }
    private void Start()
    {
        _waitForGenerate = new WaitForSeconds(_generateTime);
    }
    private IEnumerator GenerateObject()
    {
        _shouldSpawn = true;
        while (_shouldSpawn)
        {
            GameObject b = _obujectPoolRequestHub.RaiseOnGenerate();//生成

            if (b != null)
            {
                //出現位置設定
                PositioningEnemy(b);

                //出現
                b.SetActive(true);
            }
            yield return _waitForGenerate;
        }
    }
    private void PositioningEnemy(GameObject b)
    {
        //イベントを起こし，ランダムなpositionを取得
        Vector2 position = OnRequestSpawnPosition?.Invoke() ?? Vector2.zero;
        b.transform.position = position;
    }
    public void StartGenerating()
    {
        StartCoroutine(GenerateObject());
    }
    public void IncreaseSpawnRate()//スポーン間隔短縮処理
    {
        //アーチャースポーン間隔短縮
        _generateTime *= _spwanRateIncreaseValue;

        //WaitForSecondsを新たに生成
        _waitForGenerate = new WaitForSeconds(_generateTime);
    }
    public void StopGenerate()
    {
        _shouldSpawn = false;
    }
    private void PassBoneArcher(GameObject bone)
    {
        _boneBulletGenerator.InitializeBones(bone.GetComponent<BoneArcherController>());
    }
    private void PassBulletInterval(GameObject bone)
    {
        //bulletGeneratorのSetWaitForShotへ骨のBoneArcherControllerが持つ、BulletIntervalを渡す
        _boneBulletGenerator.SetWaitForShot(bone.GetComponent<BoneArcherController>().GetBulletInterval());
    }
    public uint GetPoolSize()
    {
        return _poolSize;
    }
}
