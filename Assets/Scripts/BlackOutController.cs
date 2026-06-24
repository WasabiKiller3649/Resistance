using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class BlackOutController : MonoBehaviour
{
    //イベントを受け取ってフェードインしたりアウトしたい
    [SerializeField]
    private Image _blackOutImage;

    //レベルアップイベントの参照先
    [SerializeField]
    private ExPContainer _exPContainer;

    //フェードイン，アウトしてますよ
    private bool _isFadein = false;
    private bool _isFadeout = false;

    //透明度がこれ以下になればフェードイン完了とみなす
    private const float FADEIN_END_VALUE = 0.6499f;
    private Color FADEIN_COLOR = new Color(0, 0, 0, 0.65f);

    //透明度がこれ以上になればフェードアウト完了とみなす
    private const float FADEOUT_END_VALUE = 0.1f;
    private Color FADEOUT_COLOR = new Color(0, 0, 0, 0);

    //フェードインしたりアウトする
    private FadeController _fadeController = new FadeController();
    private void OnEnable()
    {
        _exPContainer.OnNextLevel += FadeInHub;
    }
    private void FadeInHub()//コルーチンはイベントに入らないので仲介する
    {
        StartCoroutine(FadeIn());
    }
    public void FadeOutHub()
    {
        StartCoroutine(FadeOut());
    }
    private IEnumerator FadeIn()
    {
        _isFadein = true;

        //事前にフェードイン中かチェックし，trueならfalseに
        if (_isFadeout)
        {
            _isFadeout = false;
        }
        while (_blackOutImage.color.a <= FADEIN_END_VALUE && _isFadein)
        {
            //フェードインする
            Color color = _fadeController.FadeInAlpha(_blackOutImage, FADEIN_COLOR.a);
            _blackOutImage.color = color;
            yield return null;
        }
        _blackOutImage.color = FADEIN_COLOR;
        _isFadein = false;
    }
    private IEnumerator FadeOut()
    {
        _isFadeout = true;

        //事前にフェードイン中かチェックし，trueならfalseに
        if (_isFadein)
        {
            _isFadein = false;
        }
        while (_blackOutImage.color.a >= FADEOUT_END_VALUE  && _isFadeout)
        {
            //フェードアウトする
            _blackOutImage.color = _fadeController.FadeOutAlpha(_blackOutImage);
            yield return null;
        }
        _blackOutImage.color = FADEOUT_COLOR;
        _isFadeout = false;
    }
}
