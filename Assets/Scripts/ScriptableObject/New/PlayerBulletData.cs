using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/PlayerBulletData")]
public class PlayerBulletData : ScriptableObject
{
    [SerializeField] private float _moveSpeed = 0.0f;
    [SerializeField] private float _lifeTime = 0.0f;

    public float MoveSpeed => _moveSpeed;
    public float LifeTime => _lifeTime;
}