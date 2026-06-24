using UnityEngine;

public class LastBossBulletMove : MonoBehaviour, IBulletStart
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private PhisicsController _phisicsController;
    public void Attack()
    {
        //オブジェクトから見て前方向に進む
        _phisicsController.MoveSurface(transform.up * _speed);
    }
}
