using System;
using System.Collections.Generic;

public class ObjectPool<T> : IDisposable
{
    private IObjectFactory<T> _factory = default;
    private Queue<T> _standbyPool = default;
    private List<T> _usedPool = default;

    public ObjectPool(IObjectFactory<T> factory, int size)
    {
        _factory = factory;
        _standbyPool = new Queue<T>();
        _usedPool = new List<T>();

        CreatePool(size);
    }

    public void Dispose()
    {
        _standbyPool = null;
        _factory = null;
    }

    /// <summary>
    /// Poolからオブジェクトを取り出す
    /// </summary>
    /// <returns></returns>
    public T GetObject()
    {
        T instance = _standbyPool.Dequeue();
        _usedPool.Add(instance);

        instance = _factory.InitializeObject(instance);

        return instance;
    }

    /// <summary>
    /// 使い終わったオブジェクトを返す
    /// </summary>
    /// <param name="instance"></param>
    public void ReturnObject(T instance)
    {
        _usedPool.Remove(instance);
        _standbyPool.Enqueue(instance);
    }

    private void CreatePool(int size)
    {
        _standbyPool.Clear();

        for(int i = 0; i < size; i++)
        {
            T instance = _factory.GetNewObject();
            _standbyPool.Enqueue(instance);
        }
    }
}