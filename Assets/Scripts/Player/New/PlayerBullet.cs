using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private PlayerBulletData _bulletData = default;

    private float _elapsedTime = 0.0f;

    public PlayerBullet Initialize(PlayerBulletData data)
    {
        _bulletData = data;

        return this;
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        Vector2 moveDirection = transform.forward * _bulletData.MoveSpeed * deltaTime;

        transform.Translate(moveDirection);
        _elapsedTime += deltaTime;
        
        if(_elapsedTime >= _bulletData.LifeTime)
        {
            gameObject.SetActive(false);
        }
    }
}