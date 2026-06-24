using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private PhisicsController _phisicsController;
    private void OnEnable()
    {
        //オブジェクトから見て前方向に進む
        _phisicsController.MoveSurface(transform.up * _speed);
    }
}
