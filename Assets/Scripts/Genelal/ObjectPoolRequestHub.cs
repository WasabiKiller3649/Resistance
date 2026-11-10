using UnityEngine;
using System;
public class ObjectPoolRequestHub : MonoBehaviour
{
    //オブジェクト要求イベント！！！！！！！！！！！
    public event Func<GameObject> OnGenerate;
    public GameObject RaiseOnGenerate()
    {
        GameObject g = OnGenerate?.Invoke();
        return g;
    }
}
