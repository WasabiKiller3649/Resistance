using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using DG.Tweening;

public class GameClear : MonoBehaviour
{
    [SerializeField]
    private GameObject _root = default;

    [SerializeField]
    private Image _whiteImage;

    [SerializeField]
    private TextMeshProUGUI[] _messageColor;
    [SerializeField]
    private RectTransform[] _clearMessage;

    [SerializeField]
    private GameManager _gameManager;

    [Header("FadeDuration")]
    [SerializeField] private float _fadeDuration = 1.0f;

    private FadeController _fadeController = default;
    private readonly float _endFadeInValue = 0.99f;
    private bool _isEndWhiteAnimation = false;

    //クリアメッセージの移動先座標
    private readonly float _waitWhiteTime = 5f;
    private WaitForSeconds _waitForMoveText = default;
    private readonly float _textMoveInterval = 0.8f;
    private readonly float _messagePositionY = 0f;
    private readonly float _messageMoveValue = 30f;
    private bool _isEndMessageAnimation = false;

    //ラストボスが死んでから演出が始まるまでの時間
    private readonly float _waitForAnimationTime = 1f;
    private void Awake()
    {
        _fadeController = new FadeController();
        _waitForMoveText = new WaitForSeconds(_textMoveInterval);

        _root.SetActive(false);
    }

    private void Start()
    {
        _gameManager.OnLastBossDead += OnGameClear;
    }

    private void OnGameClear()
    {
        _root.SetActive(true);

        StartCoroutine(StartClearAnimation());
    }
    
    private IEnumerator StartClearAnimation()
    {
        _isEndWhiteAnimation = false;
        _isEndMessageAnimation = false;

        yield return new WaitForSeconds(_waitForAnimationTime);

        _whiteImage.DOFade(1.0f, _fadeDuration)
            .OnComplete(() => _isEndWhiteAnimation = true);

        //StartCoroutine(FadeInWhite());
        StartCoroutine(AnimationText());

        yield return new WaitUntil
            (() => _isEndMessageAnimation == true && _isEndWhiteAnimation == true);

        while (true)
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene("Title");
            }
            yield return null;
        }
    }

    private IEnumerator AnimationText()
    {
        yield return new WaitUntil(() => _isEndWhiteAnimation);

        for (int i = 0; i < _clearMessage.Length; i++)
        {
            StartCoroutine(MoveText(_clearMessage[i]));
            StartCoroutine(FadeInText(_messageColor[i]));
            yield return _waitForMoveText;
        }
        _isEndMessageAnimation = true;
    }

    private IEnumerator FadeInText(TextMeshProUGUI text)
    {
        while (text.color.a < 1)
        {
            text.color = _fadeController.FadeIn(text.color);
            yield return null;
        }
    }

    private IEnumerator MoveText(RectTransform text)
    {
        while (text.anchoredPosition.y < _messagePositionY)
        {
            text.anchoredPosition += Vector2.up * _messageMoveValue * Time.deltaTime;
            yield return null;
        }
    }
    private void OnDestroy()
    {
        if (_gameManager != null)
        {
            _gameManager.OnLastBossDead -= OnGameClear;
        }
    }
}