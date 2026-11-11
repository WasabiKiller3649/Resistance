using UnityEngine;

public class BlueZombieFactory : IObjectFactory<BlueZombie>
{
    private BlueZombie _zombiePrefab = default;
    private BlueZombieParams _zombieParams = default;
    private Transform _palyer = default;

    /// <summary>
    /// 新しく初期化済みのインスタンスを作成する
    /// </summary>
    /// <returns></returns>
    public BlueZombie GetNewObject()
    {
        BlueZombie instance = MonoBehaviour.Instantiate(_zombiePrefab);
        instance = InitializeObject(instance);

        return instance;
    }

    /// <summary>
    /// ゾンビの初期化を行う
    /// </summary>
    /// <param name="zombie"></param>
    /// <returns></returns>
    public BlueZombie InitializeObject(BlueZombie zombie)
    {
        zombie = zombie.Initialize(_zombieParams, _palyer);

        return zombie;
    }
}