using UnityEngine;
using System;
using System.Collections;
public class EnemyGenerator : MonoBehaviour, IPoolConfig
{
    //ゾンビを生成する間隔
    [SerializeField]
    private float _generateTime;
    private WaitForSeconds _waitGenerate;

    //ゾンビ生成間隔短縮
    private float _spwanRateIncreaseValue = 0.8f;

    //GameManager スクリプトがあまり太らないのでこちらに入れた
    [SerializeField]
    private GameManager _gameManager;

    //オブジェクトに渡す用
    [SerializeField]
    private GameObject _player;

    //オブジェクトを生成する数
    [SerializeField]
    private uint _poolSize;

    //オブジェクトプールを入れる
    [SerializeField]
    private ObjectPoolManager _objectPool;

    //オブジェクト生成イベント
    [SerializeField]
    private ObjectPoolRequestHub _objectPoolRequestHub;

    //敵がスポーンする座標を取得するイベント
    public event Func<Vector2> OnGetPosition;
    private void Awake()
    {
        _waitGenerate = new WaitForSeconds(_generateTime);
    }
    private void OnEnable()
    {
        //フェーズ進行イベント購読
        _gameManager.OnBeginSpawnPaceIncrement += IncreaseSpawnRate;
        _gameManager.OnBeginLastBoss += StopGenerate;

        //オブジェクトプール初期化関係
        _objectPool.OnCreatePool += SendPlayer;
    }
    private void OnDisable()
    {
        //フェーズ進行イベント購読
        _gameManager.OnBeginSpawnPaceIncrement -= IncreaseSpawnRate;
        _gameManager.OnBeginSecondCasterSpawn -= StopGenerate;

        //オブジェクトプール初期化関係
        _objectPool.OnCreatePool -= SendPlayer;
    }
    private void Start()
    {
        //pool側の準備ができ次第実行
        StartCoroutine(TryStartGenerate());
    }
    private IEnumerator TryStartGenerate()
    {
        yield return new WaitUntil(() => _objectPool.GetIsPoolReady());
        //敵を生成するよ
        StartCoroutine(GenerateCycle());
    }
    
    private IEnumerator GenerateCycle()//オブジェクトを生成する
    {
        while (true)
        {
            //オブジェクト生成
            GenerateObject();

            yield return _waitGenerate;
        }
    }
    private void GenerateObject()
    {
        //オブジェクトプールからオブジェクトを取得
        GameObject b = _objectPoolRequestHub.RaiseOnGenerate();

        //オブジェクトがnullでない
        if (b != null)
        {
            //オブジェクトの座標設定
            PositioningEnemy(b);

            //Acriveted!!!!!
            b.SetActive(true);
        }
    }
    private void PositioningEnemy(GameObject b)
    {
        //イベントを起こし，ランダムなpositionを取得
        Vector2 position = OnGetPosition?.Invoke() ?? Vector2.zero;
        b.transform.position = position;
    }
    private void SendPlayer(GameObject g)//ゾンビにPlayerを渡す
    {
        g.GetComponent<BlueZombieMove>().SetPlayer(_player);
    }
    private void IncreaseSpawnRate()
    {
        //ゾンビスポーン間隔短縮
        _generateTime *= _spwanRateIncreaseValue;
        _waitGenerate = new WaitForSeconds(_generateTime);
    }
    private void StopGenerate()
    {
        gameObject.SetActive(false);
    }
    public uint GetPoolSize()
    {
        return _poolSize;
    }
}
