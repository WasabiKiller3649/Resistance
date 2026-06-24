using UnityEngine;
using UnityEngine.UI;
public class FadeController
{
    //フェードイン、アウト時一度にどのくらい変化するか
    private const float UPDATE_RATE = 0.05f;
    public Color FadeInAlpha(Image image, float endValue, float updateRate = UPDATE_RATE)
    {
        //透明度変更前の色情報を保存
        Color color = image.color;

        //透明度を徐々に上昇
        color.a = Mathf.Lerp(color.a, endValue, updateRate);

        //変更後の色情報を返却
        return color;
    }
    public Color FadeOutAlpha(Image image, float updateRate = UPDATE_RATE)
    {
        //透明度変更前の色情報を保存
        Color color = image.color;

        //透明度を徐々に減少
        color.a = Mathf.Lerp(color.a, 0, updateRate);

        //変更後の色情報を返却
        return color;
    }

    public Color FadeOutColor(SpriteRenderer sprite, float updateRate = UPDATE_RATE)
    {
        //透明度変更前の色情報を保存
        Color color = sprite.color;

        //透明度を徐々に減少
        color.r = Mathf.Lerp(color.r, 0, updateRate);
        color.g = Mathf.Lerp(color.g, 0, updateRate);
        color.b = Mathf.Lerp(color.b, 0, updateRate);

        //変更後の色情報を返却
        return color;
    }
    public Color FadeInAlpha(SpriteRenderer sprite, float updateRate = UPDATE_RATE)
    {
        //透明度変更前の色情報を保存
        Color color = sprite.color;

        //透明度を徐々に減少
        color.a = Mathf.Lerp(color.a, 1, updateRate);

        //変更後の色情報を返却
        return color;
    }
    public Color FadeOutAlpha(SpriteRenderer sprite, float updateRate = UPDATE_RATE)
    {
        //透明度変更前の色情報を保存
        Color color = sprite.color;

        //透明度を徐々に減少
        color.a = Mathf.Lerp(color.a, 0, updateRate);

        //変更後の色情報を返却
        return color;
    }
    public Color FadeInColor(SpriteRenderer sprite, float updateRate = UPDATE_RATE)
    {
        //透明度変更前の色情報を保存
        Color color = sprite.color;

        //RGB値を徐々に上昇
        color.r = Mathf.Lerp(color.r, 1, updateRate);
        color.g = Mathf.Lerp(color.g, 1, updateRate);
        color.b = Mathf.Lerp(color.b, 1, updateRate);

        //変更後の色情報を返却
        return color;
    }
    public Color FadeIn(Color color, float updateRate = UPDATE_RATE)
    {
        //透明度を徐々に減少
        color.a = Mathf.Lerp(color.a, 1, updateRate);
        return color;
    }
}
