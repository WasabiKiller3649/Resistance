using UnityEngine;
using System;
using System.Collections.Generic;

public class ObjectPoolManager : MonoBehaviour
{
    //オブジェクトプールのオブジェクトを格納
    private List<PooledObject> _pooledObjects;

    //enemyに渡す初期化に必要な処理を通知
    public event Action<GameObject> OnCreatePool;

    //初期化処理
    public event Action<GameObject> OnPoolReady;

    //poolの初期化完了
    private bool _isPoolReady = false;

    //生成するオブジェクト
    [SerializeField]
    private GameObject _generateObject;

    //Generaterイベント
    [SerializeField]
    private ObjectPoolRequestHub _objectPoolRequestHub;
    private void OnEnable()
    {
        //イベント購読
        _objectPoolRequestHub.OnGenerate += ServeObject;
    }
    private void Start()
    {
        SetUpPool();
    }
    private GameObject ServeObject()
    {
        //使えるオブジェクトがない場合はnull
        GameObject b = CheckPool();
        return b;
    }
    private GameObject CheckPool()//オブジェクトを生成できるか確認
    {
        GameObject b = null;
        //先頭からリストを探索し非アクティブのオブジェクトをアクティブにする
        foreach (PooledObject p in _pooledObjects)
        {
            if (p == null) continue;
            if (!p.GetIsActive())
            {
                b = p.gameObject;
                break;
            }
        }
        //使えるオブジェクトがある場合はそれを返す,ない場合はnull
        return b;
    }
    private void SetUpPool()
    {
        //リストを初期化
        _pooledObjects = new List<PooledObject>();

        //生成するpoolの大きさを取得
        int poolsize = default;
        if (gameObject.TryGetComponent<IPoolConfig>(out IPoolConfig poolconfig))
        {
            poolsize = (int)poolconfig.GetPoolSize();
        }

        GameObject g = null;
        //プールサイズの分だけ生成＆初期化
        for (int i = 0; i < poolsize; i++)
        {
            g = Instantiate(_generateObject);
            _pooledObjects.Add(g.GetComponent<PooledObject>());
            OnCreatePool?.Invoke(g);//初期化に必要な処理を入れる

            //生成したオブジェクトを無効化する、しないとおかしくなる
            g.SetActive(false);
        }

        //初期化処理
        OnPoolReady?.Invoke(g);
        //poolの初期化完了
        _isPoolReady = true;
    }
    public bool GetIsPoolReady()
    {
        return _isPoolReady;
    }
}
