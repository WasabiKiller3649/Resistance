using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private readonly ObjectPool<BlueZombie> _pool = default;
    private readonly SpawnPositionProvider _spawnPositionProvider = default;
    /// <summary>スポーン範囲を指定する</summary>
    private BoxCollider2D _spawnCollider = default;
    private float _spawnInterval = 0.0f;
    private float _elapsedTime = 0.0f;
    private Vector2 _spawnRange = default;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if(_elapsedTime >= _spawnInterval)
        {
            _elapsedTime = 0.0f;

            Spawn();
        }
    }

    private void Spawn()
    {
        Vector2 spawnPosition = _spawnPositionProvider.GetRandomPosition();
        BlueZombie instance = _pool.GetObject();
        instance.transform.position = spawnPosition;

        instance.Appearance();
    }
}