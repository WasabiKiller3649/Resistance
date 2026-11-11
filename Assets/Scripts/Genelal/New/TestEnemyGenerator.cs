using UnityEngine;

public class TestEnemyGenerator : MonoBehaviour
{
    [SerializeField] private BlueZombie _zombie = default;
    [SerializeField] private BlueZombieParams _params = default;
    [SerializeField] private Transform _player = default;


    private void Awake()
    {
        _zombie.Initialize(_params, _player);
        _zombie.Appearance();
    }
}