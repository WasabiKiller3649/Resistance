using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/BlueZombieParams")]
public class BlueZombieParams : ScriptableObject
{
    [SerializeField] private float _moveSpeed = 0.0f;
    [SerializeField] private float _attackVolume = 0.0f;
    [SerializeField] private float _initialeHealth = 0.0f;

    public float MoveSpeed => _moveSpeed;
    public float AttackVolume => _attackVolume;
    public float InitialeHealth => _initialeHealth;
}