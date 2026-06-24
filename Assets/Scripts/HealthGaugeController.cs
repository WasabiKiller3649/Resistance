using UnityEngine;
using System.Collections;
public class HealthGaugeController : MonoBehaviour
{
    //実際の体力量を表すゲージ
    [SerializeField]
    private Transform _frontGaugeScale;

    //受けてから反映されるまでのラグを表すゲージ
    [SerializeField]
    private Transform _middleGaugeScale;

    //最大HPと現在HPを参照するイベント
    [SerializeField]
    private UpdateHealthEventHub _updateHealthEventHub;

    //ゲージのX,Y値を保存する
    private float _gaugeVerticalValue;
    private float _gaugeHorizontalValue;

    //ゲージのサイズを計算する
    private UpdateGaugeSize _updateGaugeSize;
    private void Awake()
    {
        _updateGaugeSize = new UpdateGaugeSize();

        //各ゲージのX,YScaleはFrontGaugeを基準にする
        _gaugeVerticalValue = _frontGaugeScale.localScale.y;
        _gaugeHorizontalValue = _frontGaugeScale.localScale.x;
    }
    private void OnEnable()
    {
        _updateHealthEventHub.OnUpdateHealth += UpdateGaugeHub;
    }
    private void UpdateGaugeHub(int maxValue, int currentValue)
    {
        if (currentValue < 0)
        {
            return;//体力値がマイナス（すでに、死んでいる）の時は、処理をしない
        }
        else
        {
            //変更後のゲージサイズを取得
            float nextGaugeSize = _updateGaugeSize.CalcuratePercentage(_gaugeHorizontalValue,
                maxValue, currentValue);

            //変更後のゲージサイズを各ゲージにわたし、反映

            UpdateHealthUI(nextGaugeSize);
            StartCoroutine(UpdateMiddleGauge(nextGaugeSize));
        }
    }
    private void UpdateHealthUI(float nextGaugeSize)
    {
        //体力ゲージの表示を更新
        _frontGaugeScale.localScale = new Vector2(nextGaugeSize, _gaugeVerticalValue);
    }
    private IEnumerator UpdateMiddleGauge(float nextGaugeSize)
    {
        while (_middleGaugeScale.localScale.x > nextGaugeSize)
        {
            float previousX = _middleGaugeScale.localScale.x;
            _middleGaugeScale.localScale = new Vector2(previousX - Time.deltaTime,
                _gaugeVerticalValue);
            yield return null;
        }
        _middleGaugeScale.localScale = new Vector2(nextGaugeSize, _gaugeVerticalValue);
    }
}
