using UnityEngine;
using System.Collections.Generic;
public class ExPPoolManager : MonoBehaviour
{
    //オブジェクトプールのオブジェクトを格納
    private List<PooledObject> _pooledObjects;

    //生成するオブジェクトの数
    [SerializeField]
    private uint _poolsize;

    //ExPSpawnイベント参照先
    [SerializeField]
    private ExPGenerator _generator;

    private enum ExPType
    {
        Small,
        Lerge,
    }
    //インスペクターから生成するExPを選択
    [SerializeField]
    private ExPType _exPType;

    //生成するオブジェクト
    [SerializeField]
    private GameObject _exP;
    private void Start()
    {
        SetUpPool();
    }
    private void OnEnable()
    {
        switch(_exPType)
        {
            case ExPType.Small:
                _generator.OnSmallExpSpawned += SpawnExP;
                break;
            case ExPType.Lerge:
                _generator.OnLergeExPSpawned += SpawnExP;
                break;
        }
    }
    private GameObject SpawnExP()
    {
        GameObject b = null;

        //返り値に使える敵、ない場合はnull
        b = CheckPool();

        if (b == null)
        {
            //使えるオブジェクトがない場合は新たに追加
            print("Pool追加");
            CreatPool();
            b = CheckPool();
        }
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
    private void SetUpPool()
    {
        //リストを初期化
        _pooledObjects = new List<PooledObject>();

        //プールサイズの分だけ生成＆初期化
        for (int i = 0; i < _poolsize; i++)
        {
            //オブジェクトプールに一体追加
            CreatPool();
        }
    }
    private void CreatPool()
    {
        //オブジェクトを追加
        GameObject g;
        g = Instantiate(_exP);
        _pooledObjects.Add(g.GetComponent<PooledObject>());
        g.SetActive(false);
    }
}
