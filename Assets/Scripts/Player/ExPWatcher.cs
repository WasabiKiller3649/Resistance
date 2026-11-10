using UnityEngine;
using System.Collections;
public class ExPWatcher : MonoBehaviour
{
    //経験値探索範囲
    private float _exPWatchRadius = 6;

    //探索物を格納する配列
    private Collider2D[] _exPs;
    [SerializeField]
    private LayerMask _exPLayer;

    //OverLapCircleを起動する間隔
    [SerializeField]
    private float _searchInterval;
    private WaitForSeconds _waitInterval;
    private void Awake()
    {
        _exPs = new Collider2D[1000];
        _waitInterval = new WaitForSeconds(_searchInterval);
    }
    private void OnEnable()
    {
        StartCoroutine(SearchExP());
    }
    private IEnumerator SearchExP()
    {
        while (true)
        {
            yield return _waitInterval;
            //範囲内の経験値の数
            int hitCount;
            //範囲内のコライダーを全探索
            hitCount = Physics2D.OverlapCircleNonAlloc
                (transform.position, _exPWatchRadius, _exPs, _exPLayer);

            //範囲内の経験値に吸い寄せ開始命令
            for (int i = 0; i < hitCount; i++)
            {
                //取得したコライダーにIAttractableがついていれば，それをattractableに格納しif文突入
                if (_exPs[i].gameObject.TryGetComponent<IAttractable>(out IAttractable attractable))
                {
                    //インターフェースに存在するメソッドのみ呼び出し可
                    attractable.StartAttract(transform);
                }
            }
        }
    }
}
