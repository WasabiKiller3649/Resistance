using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
public class GameOver : MonoBehaviour
{
    [SerializeField]
    private GameObject _root = default;

    //çïîwåi
    [SerializeField]
    private Image _backGround;
    private WaitForSeconds _waitForFadeIn = default;
    private readonly float _fadeInWaitTime = 0.1f;

    //éÄñSéûÇÃÉÅÉbÉZÅ[ÉW
    [SerializeField]
    private RectTransform _messageTransform;
    [SerializeField]
    private TextMeshProUGUI _messageText;
    private readonly Vector2 _messageEndPosition = new Vector2(31, 118);
    [SerializeField]
    private float _directionalTime = default;
    private WaitForSeconds _waitForDirection = default;

    //ÉCÉxÉìÉgçwì«
    [SerializeField]
    private HPController _hpController;
    [SerializeField]
    private GameManager _gameManager;

    private FadeController _fadeController = default;
    private void Awake()
    {
        _fadeController = new FadeController();
        _waitForFadeIn = new WaitForSeconds(_fadeInWaitTime);
        _waitForDirection = new WaitForSeconds(_directionalTime);

        _hpController.OnDeath += Death;
        _gameManager.OnLastBossDead += OnClear;

        //if (_root != null && _root.activeSelf)
        //{
        //    _root.SetActive(false);
        //}
        gameObject.SetActive(false);
    }
    private void OnClear()
    {
        _hpController.OnDeath -= Death;
        _gameManager.OnLastBossDead -= OnClear;
    }
    private void Death()
    {
        gameObject.SetActive(true);

        StartCoroutine(UpBackGround());
        StartCoroutine(FadeInText());
        StartCoroutine(MoveText());
        StartCoroutine(Return());
    }
    private IEnumerator Return()
    {
        yield return _waitForDirection;

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("MainGame");
            }
            yield return null;
        }
    }
    private IEnumerator UpBackGround()
    {
        while (_backGround.color.a < 1)
        {
            yield return _waitForFadeIn;
            _backGround.color = _fadeController.FadeInAlpha(_backGround, 1);
            yield return null;
        }
    }
    private IEnumerator MoveText()
    {
        while (_messageTransform.anchoredPosition.y > _messageEndPosition.y)
        {
            _messageTransform.anchoredPosition += Vector2.down * Time.deltaTime;
            
            yield return null;
        }
    }
    private IEnumerator FadeInText()
    {
        while (_messageText.color.a < 1)
        {
            yield return _waitForFadeIn;
            _messageText.color = _fadeController.FadeIn(_messageText.color);
            yield return null;
        }
    }
}
