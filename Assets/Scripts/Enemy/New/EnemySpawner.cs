using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    /// <summary>スポーン範囲を指定する</summary>
    [SerializeField] private BoxCollider2D _spawnCollider = default;
    [SerializeField] private BlueZombie _zombiePrefab = default;
    [SerializeField] private BlueZombieParams _params = default;
    [SerializeField] private Transform _player = default;
    [SerializeField] private float _spawnInterval = 0.0f;
    [SerializeField] private int _poolSize = 0;

    private BlueZombieFactory _factory = default;
    private SpawnPositionProvider _spawnPositionProvider = default;
    private ObjectPool<BlueZombie> _pool = default;
    private float _elapsedTime = 0.0f;

    private void Awake()
    {
        Initialize();
    }

    private EnemySpawner Initialize()
    {
        _spawnPositionProvider = new SpawnPositionProvider(_spawnCollider);
        _factory = new BlueZombieFactory(_zombiePrefab, _params, _player);
        _pool = new ObjectPool<BlueZombie>(_factory, _poolSize);

        return this;
    }

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
        if(!_pool.TryGetObject(out BlueZombie instance))
        {
            return;
        }

        Vector2 spawnPosition = _spawnPositionProvider.GetRandomPosition();
        instance.transform.position = spawnPosition;

        instance.Appearance();
    }
}