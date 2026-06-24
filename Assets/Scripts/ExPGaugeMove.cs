using UnityEngine;

public class ExPGaugeMove : MonoBehaviour
{
    [SerializeField]
    private Transform _homePosition;
    [SerializeField]
    private RectTransform _transform;
    [SerializeField]
    private Camera _mainCamera;
    private float _followSmooth = 0.1f;
    private void LateUpdate()
    {
        Move();
    }
    private void Move()
    {
        if (transform.position == _homePosition.position) return;

        //ワールド座標をスクリーン座標に変換
        Vector2 target = _mainCamera.WorldToScreenPoint(_homePosition.position);

        //スクリーン座標をCanvas座標に変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_transform, target, _mainCamera, out target);

        //Canvas座標をtargetに設定し、Move
        _transform.anchoredPosition = Vector2.Lerp(_transform.anchoredPosition, target, _followSmooth);
    }
}
