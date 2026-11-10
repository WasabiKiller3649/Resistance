using UnityEngine;

public class GetSpawnPosition : MonoBehaviour
{
    //コライダーの範囲内でスポーンさせる
    [SerializeField]
    private BoxCollider2D _spawnRange;

    //イベントの参照先
    [SerializeField]
    private EnemyGenerator _enemyGenerator;
    [SerializeField]
    private BoneArcherGenerator _archer;
    void Start()
    {
        //OnTrySpawnにGetRandomPositionを登録
        if (_enemyGenerator != null)
        {
            _enemyGenerator.OnGetPosition += GetRandomPosition;
        }
        if (_archer != null)
        {
            _archer.OnRequestSpawnPosition += GetRandomPosition;
        }
    }
    private Vector2 GetRandomPosition()
    {
        Vector2 position = default;
        Bounds bounds = _spawnRange.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        position = new Vector2(x, y);
        return position;
    }
}
