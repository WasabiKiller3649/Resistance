using UnityEngine;

public class BoneMoveDirectionChange : MonoBehaviour
{
    [SerializeField]
    private BoneArcherController _controller;

    //ˆÚ“®•ûŒü‚ğŠi”[‚·‚é
    private Vector2[] _moveVector;
    private void Awake()
    {
        _controller.OnRequestDirection += DirectionChanger;
    }
    private Vector2 DirectionChanger(Vector2 v)
    {
        //1-4‚Ìƒ‰ƒ“ƒ_ƒ€
        int i = Random.Range(0, 4);
        v = _moveVector[i];
        return v;
    }
    public void SetMoveVector(float speed)
    {
        //”z—ñ‰Šú‰»
        _moveVector = new Vector2[4]
        {
            new Vector2(1f * speed, 0f),//‰E
            new Vector2(-1f * speed, 0f),//¶
            new Vector2(0f, 1f * speed),//ã
            new Vector2(0f, -1f * speed),//‰º
        };
    }
}
