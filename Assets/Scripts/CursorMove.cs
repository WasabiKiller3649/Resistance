using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
public class CursorMove : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private RectTransform _cursorRectTransform;

    [SerializeField]
    private RectTransform _selfRectTransform;
    private const float CURSOR_DISTENCE = -100;
    private const float DISTENCE_UPDATE_RATE = 0.05f;
    private WaitUntil _waitUntilPosition = default;

    [SerializeField]
    private CursorState _cursorState;
    [SerializeField]
    private CursorState.PositionState _positionState;

    //ˆÚ“®æ‚Æ‚Ì‹——£‚ª‚±‚êˆÈ‰º‚É‚È‚ê‚Î“ž’…‚Æ‚Ý‚È‚·
    private const float MOVE_END_DISTENCE = 5f;
    private void Awake()
    {
        _waitUntilPosition = new WaitUntil(() => _cursorState.State == _positionState);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(Move());
        _cursorState.State = _positionState;
    }
    private IEnumerator Move()
    {
        Vector2 goalPosition = _selfRectTransform.anchoredPosition + Vector2.right * CURSOR_DISTENCE;
        while (true)
        {
            yield return _waitUntilPosition;

            Vector2 nextPosition = Vector2.Lerp(_cursorRectTransform.anchoredPosition,
                goalPosition, DISTENCE_UPDATE_RATE);
            _cursorRectTransform.anchoredPosition = nextPosition;
            if (Vector2.Distance(_cursorRectTransform.anchoredPosition, goalPosition) <= MOVE_END_DISTENCE)
            {
                break;
            }
            yield return null;
        }
        _cursorRectTransform.anchoredPosition = goalPosition;
    }
}
