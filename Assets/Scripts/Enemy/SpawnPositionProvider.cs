using UnityEngine;

public class SpawnPositionProvider
{
    //コライダーの範囲内でスポーンさせる
    private BoxCollider2D _spawnRange = default;

    public SpawnPositionProvider(BoxCollider2D spawnRange)
    {
        _spawnRange = spawnRange;
    }

    public Vector2 GetRandomPosition()
    {
        Bounds bounds = _spawnRange.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        Vector2 position = new Vector2(x, y);
        return position;
    }
}
