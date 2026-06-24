using UnityEngine;
using UnityEngine.UI;

public class SkillIconSetter : MonoBehaviour
{
    //
    [SerializeField]
    private Image _image;
    public void SetSkillIcon(Sprite sprite)//スキルのアイコンを入れる
    {
        _image.sprite = sprite;
    }

}
