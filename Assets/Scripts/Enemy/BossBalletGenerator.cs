using UnityEngine;
using System;
public class BossBulletGenerator : MonoBehaviour, IPoolConfig
{
    [SerializeField]
    private uint _poolSize;
    [SerializeField]
    private ObjectPoolRequestHub _objectPoolRequestHub;
    public void Subscribe(Func<GameObject> function)
    {
        function += InvokeRequestEvent;
    }
    private GameObject InvokeRequestEvent()
    {
        return _objectPoolRequestHub.RaiseOnGenerate();
    }
    public uint GetPoolSize()
    {
        return _poolSize;
    }
}
