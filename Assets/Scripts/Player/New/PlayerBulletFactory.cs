using UnityEngine;

public class PlayerBulletFactory 
{
    private PlayerBulletData _bulletData = default;
    private PlayerBullet _bullet = default;

    public PlayerBullet GetBullet()
    {
        PlayerBullet instance = MonoBehaviour.Instantiate(_bullet);
        return instance.Initialize(_bulletData);
    }
}