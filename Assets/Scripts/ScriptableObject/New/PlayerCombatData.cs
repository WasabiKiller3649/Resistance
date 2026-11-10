using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/PlayerCombatData")]
public class PlayerCombatData : ScriptableObject
{
    [SerializeField] private float _attackInterval = 0.0f;
    [SerializeField] private float _baseDamage = 0.0f;
    [SerializeField] private float _attackRadius = 0.0f;

    public float AttackInterval => _attackInterval;
    public float BaseDamage => _baseDamage;
    public float AttackRadius => _attackRadius;
}