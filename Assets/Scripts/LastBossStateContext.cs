using TMPro;
using UnityEngine;

public class LastBossStateContext
{
    public LastBossStateContext(LastBossController controller, Transform playerTransform,
        ObjectPoolRequestHub hub, LastBossAnimatorController anim, GameObject[] weapons)
    {
        boss = controller;
        this.playerTransform = playerTransform;
        objectPoolRequestHub = hub;
        animatorController = anim;
        this.weapons = weapons;
        fadeController = new FadeController();
        weaponSpriteRenderers = new SpriteRenderer[weapons.Length];
        ringPositionGenerator = new RingPositionGenerator();
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].TryGetComponent<SpriteRenderer>(out var sprite))
            {
                weaponSpriteRenderers[i] = sprite;
            }
        }
    }
    public LastBossController boss;
    public Transform playerTransform;
    public ObjectPoolRequestHub objectPoolRequestHub;
    public LastBossAnimatorController animatorController;
    public GameObject[] weapons;
    public SpriteRenderer[] weaponSpriteRenderers;
    public RingPositionGenerator ringPositionGenerator;

    //武器をFadeInさせる
    public FadeController fadeController;

    //色の値がこれ以上になったらフェードイン終了とする
    public const float FADEIN_END_VALUE = 0.94f;
    //値がこれ以下になったらフェードアウト終了とする
    public const float FADEOUT_END_VALUE = 0.04f;
    //武器のSpriteの最大、最小の値
    private Color _maxColor = new Color(1, 1, 1, 1);
    private Color _minColor = new Color(0, 0, 0, 0);
    public void MaximizeColor(SpriteRenderer sprite)
    {
        sprite.color = _maxColor;
    }
    public void MinimizeColor(SpriteRenderer sprite)
    {
        sprite.color = _minColor;
    }
    //弾の角度を補正
    public const float OFFSET_BULLET_ANGLE = -90;
    //武器の角度設定時に使う補正値
    public const float OFFSET_WEAPON_ANGLE = 180;
}
