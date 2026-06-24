using UnityEngine;

public class SpriteRendererController : MonoBehaviour
{
    //‰ŠúF
    [SerializeField]
    private Color _initialColor;
    [SerializeField]
    private SpriteRenderer _sprite;
    private void OnEnable()
    {
        _sprite.color = _initialColor;
    }
    private void OnDisable()
    {
        _sprite.color = _initialColor;
    }
}
