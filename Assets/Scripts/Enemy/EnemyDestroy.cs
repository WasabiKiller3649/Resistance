using UnityEngine;

public class EnemyDestroy : MonoBehaviour, IBreakable
{
    //死んだときに放出する経験値の数
    [SerializeField]
    private int _smallExpAmount;

    [SerializeField]
    private int _lergeExPAmount;
    public void Destroy()
    {
        //DeathEventHubに死んだことを通知
        DeathEventHub.RaiseDeath(transform.position, _smallExpAmount, _lergeExPAmount);
        gameObject.SetActive(false);
    }
}
