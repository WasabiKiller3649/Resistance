using UnityEngine;
using UnityEngine.EventSystems;
public class MousePointNotice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //スキル効果の説明文をこれに渡す
    [SerializeField]
    private SkillDescriptionManager _skillDescriptionManager;

    //抽選されたスキルデータを参照する
    [SerializeField]
    private SkillHolder _skillHolder;

    //マウスが通過した時の音
    [SerializeField]
    private AudioSource _audioSource;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //マウスポインターが合った時、スキルの説明文を画面に表示する
        _skillDescriptionManager.SetetSkillDescription
            (_skillHolder._skillData.GetSkillEffectDescription());

        //音再生
        _audioSource.Play();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //マウスポインターが離れた時、説明文を空にする
        _skillDescriptionManager.ClearText();
    }
}
