using UnityEngine;
using System.Collections.Generic;
public class PlayerBulletPoolManager : MonoBehaviour
{
    //オブジェクトプールのオブジェクトを格納
    private List<PooledObject> _pooledObjects;

    //生成するオブジェクトの数
    [SerializeField]
    private uint _poolsize;

    //生成するオブジェクト（弾）
    [SerializeField]
    private GameObject _bullet;

    //弾に渡すダメージデータ
    [SerializeField]
    private DamageManager _bulletDamageManager;

    //弾発射イベント参照先
    [SerializeField]
    private ObjectPoolRequestHub _objectPoolRequestHub;
    private void Awake()
    {
        SetUpPool();
    }
    private void Start()
    {
        _objectPoolRequestHub.OnGenerate += ShootBullet;
    }

    private GameObject ShootBullet()
    {
        GameObject b = null;

        //使える弾が返ってくる、ない場合はnull
        b = GetBullet();

        return b;
    }
    private GameObject GetBullet()//撃ち出すメソッド
    {
        GameObject b = null;
        //先頭からリストを探索し非アクティブのオブジェクトをアクティブにする
        foreach (PooledObject p in _pooledObjects)
        {
            if (!p.GetIsActive())
            {
                b = p.gameObject;
                break;
            }
        }
        //使える弾がある場合はそれを返す,ない場合はnull
        return b;
    }
    private void SetUpPool()
    {
        //リストを初期化
        _pooledObjects = new List<PooledObject>();

        //プールサイズの分だけ生成＆初期化
        for (int i = 0; i < _poolsize; i++)
        {
            GameObject g;
            g = Instantiate(_bullet);
            _pooledObjects.Add(g.GetComponent<PooledObject>());

            //ダメージデータ受け渡し
            g.GetComponent<PlayerDamageServer>().SetDamageManager(_bulletDamageManager);
            g.SetActive(false);
        }
    }
}
