using UnityEngine;
using System.Collections.Generic;
public class BulletTargetFinder : MonoBehaviour
{
    //オーバーラップサークルの返り値を格納
    private Collider2D[] _enemys;

    //Enemyレイヤー
    [SerializeField]
    private LayerMask _enemyLayer;
    //探索範囲の半径
    [SerializeField]
    private float _finderRadius;

    //イベント参照先
    [SerializeField]
    private PlayerBulletGenerator _generator;
    private void Awake()
    {
        _enemys = new Collider2D[60];
    }
    private void OnEnable()
    {
        _generator.OnRequestTarget += SearchNearEnemy;
    }
    private Collider2D SearchNearEnemy()
    {
        //敵を探索
        int hitCount = Physics2D.OverlapCircleNonAlloc
                (transform.position, _finderRadius, _enemys, _enemyLayer);

        //返すコライダー
        Collider2D target = null;

        //範囲内に敵がいる場合
        if (hitCount > 0)
        {
            //配列の中の最小値を求めるためでかい数値を用意
            float distance = 999;

            //一番近い敵を探索
            for (int i = 0; i < hitCount; i++)
            {
                //自分の位置と敵の位置を比較
                float distanceTMP = 
                    Vector3.Distance(transform.position, _enemys[i].transform.position);

                //一つ前のコライダーより近ければ距離を保存
                if (distance > distanceTMP)
                {
                    //探索用の距離
                    distance = distanceTMP;

                    //返り値を保存
                    target = _enemys[i];
                }
            }
        }
        return target;
    }
}
