using UnityEngine;
using System.Collections.Generic;
public class FireBulletPoolManager : MonoBehaviour
{
    //オブジェクトを格納
    private List<PooledObject> _pooledObjects;

    //生成するオブジェクトの数
    [SerializeField]
    private uint _poolSize;

    //生成するオブジェクト
    [SerializeField]
    private GameObject _bullet;

    //発射イベントの参照先
    [SerializeField]//Caster一人目
    private FireBulletGenerator _firstGenerator;

    [SerializeField]//Caster二人目
    private FireBulletGenerator _secondGenerator;
    private void Awake()
    {
        _firstGenerator.OnRequestFireBullet += GetPoolObject;
        _secondGenerator.OnRequestFireBullet += GetPoolObject;
    }
    private void Start()
    {
        SetUpPool();
    }
    private GameObject GetPoolObject()
    {
        GameObject b = null;

        //返り値に使えるオブジェクト、ない場合はnull
        b = CheckPool();
        return b;
    }
    private GameObject CheckPool()//オブジェクトを生成できるか確認
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

        //使えるオブジェクトがある場合はそれを返す,ない場合はnull
        return b;
    }
    void SetUpPool()
    {
        //リストを初期化
        _pooledObjects = new List<PooledObject>();

        //プールサイズの分だけ生成＆初期化
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject g;

            //bulletをInstantiateする
            g = Instantiate(_bullet);

            //探索用リストに追加
            _pooledObjects.Add(g.GetComponent<PooledObject>());

            g.SetActive(false);
        }
    }
}
