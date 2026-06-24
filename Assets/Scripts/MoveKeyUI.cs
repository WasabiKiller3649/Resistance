using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class MoveKeyUI : MonoBehaviour
{
    [Header("テキスト")]
    [SerializeField] private TextMeshProUGUI _message = default;
    [SerializeField] private RectTransform _standbyPosition = default;
    [SerializeField] private RectTransform _noticePosition = default;
    private Color _transparent = default;//透明
    private Color _colorWhite = default;//KeyImageとテキストの色
    private Color _colorBrack = default;//Key背景の色

    [Header("KeyImage")]
    [SerializeField] private Image[] _keyImages = default;
    [SerializeField] private Image[] _keyBackImages = default;

    [Header("Player")]
    [SerializeField] private PlayerController _player = default;
    private float _unMoveTime = default;
    private Vector2 _playerSpeed = default;

    [Header("UIを表示しだす時間")]
    [SerializeField] private float _noticeTime = 8f;

    [Header("UIがフェードする時間")]
    [SerializeField] private float _fadeTime = 0.5f;

    private void Awake()
    {
        _transparent = new Color(0f, 0f, 0f, 0f);
        _colorWhite = new Color(1f, 1f, 1f, 1f);
        _colorBrack = new Color(0f, 0f, 0f, 1f);

        //テキスト初期化
        _message.rectTransform.anchoredPosition = _standbyPosition.anchoredPosition;
        _message.color = _transparent;

        //キー初期化
        for (int i = 0; i < _keyImages.Length; i++)
        {
            if (_keyImages[i] != null)
            {
                _keyImages[i].color = _transparent;
                _keyBackImages[i].color = _transparent;
            }
        }
    }
    private void OnEnable()
    {
        _player.OnStartMove += HideUI;
    }
    private void Update()
    {
        if (_player == null) return;

        CountUnMoveTime();//移動していない時間を計測
    }
    private void CountUnMoveTime()
    {
        Vector2 prevSpeed = _playerSpeed;
        _playerSpeed = _player.MoveSpeed;

        //動き出したとき
        if (prevSpeed == Vector2.zero && _playerSpeed != Vector2.zero)
        {
            _unMoveTime = 0;
            HideUI();
        }

        float prevTime = _unMoveTime;
        _unMoveTime += Time.deltaTime;

        //動いてないとき
        if (_playerSpeed == Vector2.zero)
        {
            if (prevTime < _noticeTime && _noticeTime <= _unMoveTime)
            {
                //UIを表示
                ShowUI();
            }
        }
    }
    private void ShowUI()
    {
        for (int i = 0; i < _keyImages.Length; i++)
        {
            _keyImages[i].DOColor(_colorWhite, _fadeTime);
            _keyBackImages[i].DOColor(_colorBrack, _fadeTime);
        }

        _message.DOColor(_colorWhite, _fadeTime);
        _message.rectTransform.DOAnchorPos(_noticePosition.anchoredPosition, _fadeTime);
    }
    private void HideUI()
    {
        for (int i = 0; i < _keyImages.Length; i++)
        {
            _keyImages[i].DOColor(_transparent, _fadeTime);
            _keyBackImages[i].DOColor(_transparent, _fadeTime);
        }

        _message.DOColor(_transparent, _fadeTime);
        _message.rectTransform.DOAnchorPos(_standbyPosition.anchoredPosition, _fadeTime);
    }
}
