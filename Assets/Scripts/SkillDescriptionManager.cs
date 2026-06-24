using UnityEngine;
using TMPro;
public class SkillDescriptionManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textMeshPro;
    //スキルの説明文を受け取り反映する
    public void SetetSkillDescription(string skillDescription)
    {
        _textMeshPro.text = skillDescription;
    }
    //マウスポインターが離れた時にテキストボックスを空にする
    public void ClearText()
    {
        _textMeshPro.text = string.Empty;
    }
}
