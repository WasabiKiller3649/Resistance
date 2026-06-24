using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    private enum MenuState
    {
        OpenMenu,
        CloseMenu,
    }
    private MenuState _menuState = MenuState.CloseMenu;

    [Header("イベント参照")]
    [SerializeField] private ExPContainer _exPContainer = default;

    [Header("UI")]
    [SerializeField] private Image _black = default;
    [SerializeField] private TextMeshProUGUI _message = default;
    [SerializeField] private GameObject _textBack = default;
    [SerializeField] private GameObject _button = default;

    private Color _transparent = default;
    private Color _blackColor = default;
    private Color _messageColor = default;

    private float _beforeStopTimeScale = default;//ポーズ前のTimeScale
    private bool _isLevelUp = false;
    private void Awake()
    {
        _blackColor = new Color(0f, 0f, 0f, 188f / 255f);
        _messageColor = new Color(1f, 1f, 1f, 1f);
        Time.timeScale = 1;

        _black.color = _transparent;
        _message.color = _transparent;
        _button.SetActive(false);
        _textBack.SetActive(false);
    }
    private void OnEnable()
    {
        _exPContainer.OnNextLevel += StartLevelUP;
    }

    private void OnDisable()
    {
        _exPContainer.OnNextLevel -= StartLevelUP;
    }
    private void Update()
    {
        if (_isLevelUp) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (_menuState)
            {
                case MenuState.CloseMenu:
                    OpenMenu();
                    break;
                case MenuState.OpenMenu:
                    CloseMenu();
                    break;
            }
        }
    }
    private void OpenMenu()
    {
        //UI表示
        _black.color = _blackColor;
        _message.color = _messageColor;
        _button.SetActive(true);
        _textBack.SetActive(true);

        _beforeStopTimeScale = Time.timeScale;
        Time.timeScale = 0;

        _menuState = MenuState.OpenMenu;
    }
    private void CloseMenu()
    {
        //UI非表示
        _black.color = _transparent;
        _message.color = _transparent;
        _button.SetActive(false);
        _textBack.SetActive(false);

        Time.timeScale = _beforeStopTimeScale;

        _menuState = MenuState.CloseMenu;
    }
    private void StartLevelUP()
    {
        _isLevelUp = true;
    }
    public void EndLevelUP()
    {
        _isLevelUp = false;
    }
    public void ReturnTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
