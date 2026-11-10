using UnityEngine;
using System;
public class LastBossDestroy : MonoBehaviour, IBreakable
{
    //死んだときに放出する経験値の数
    [SerializeField]
    private int _smallExpAmount;

    [SerializeField]
    private int _lergeExPAmount;

    public event Action OnDestroy;
    void IBreakable.Destroy()
    {
        //DeathEventHubに死んだことを通知
        DeathEventHub.RaiseDeath(transform.position, _smallExpAmount, _lergeExPAmount);
        OnDestroy?.Invoke();
        GetComponent<Collider2D>().enabled = false;
    }
}
