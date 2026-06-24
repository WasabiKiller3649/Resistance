using UnityEngine;
public class BlueZombieMove : MonoBehaviour
{
    //プレイヤーに向かう速度
    [SerializeField]
    private float _moveSpeed;

    //プレイヤーオブジェクト
    private GameObject _player;

    private enum MoveState
    {
        Move,
        Stop,
    }
    private MoveState _state = MoveState.Stop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case MoveState.Move:
                float time = Time.deltaTime;
                Vector2 a = transform.position;
                transform.position = Vector3.MoveTowards(
                    a, _player.transform.position, _moveSpeed * time);
                break;
            case MoveState.Stop:
                break;
        }
    }
    public void ReMove()
    {
        _state = MoveState.Move;
    }
    private void OnDisable()
    {
        _state = MoveState.Stop;
    }
    public void SetPlayer(GameObject p)
    {
        _player = p;
    }
}
