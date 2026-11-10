using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ExPGaugeController : MonoBehaviour
{
    [SerializeField]
    private Image _outerFrame;
    [SerializeField]
    private Image _innerGreen;

    //イベント参照先
    [SerializeField]
    private ExPContainer _exPContainer;

    //画像が完全な円形ではないので，FillAmountの最大値を明示的に設定
    private const float MAX_FILL_AMOUNT = 0.75f;

    //ゲージの大きさを変える
    private UpdateGaugeSize _updateGaugeSize;

    //Mathf.Lerpでゲージをじわじわ変更させる
    private const float GAUGE_SIZE_PROGRESS = 0.1f;

    //変更完了とみなす値
    private const float CHENGE_END_VALUE = 0.1f;
    private void Awake()
    {
        RemoveGaugeSize();//ゲージ初期化
        _updateGaugeSize = new UpdateGaugeSize();
    }
    private void OnEnable()
    {
        _exPContainer.OnUpdateExP += UpdateGauge;
        _exPContainer.OnNextLevel += RemoveGaugeSize;
    }
    private void UpdateGauge(float maxValue, float currentValue)
    {
        //渡された値からゲージの割合を出し，ゲージのサイズを算出する
        float currentGaugeSize =
            _updateGaugeSize.CalcuratePercentage(MAX_FILL_AMOUNT, maxValue, currentValue);

        StartCoroutine(ChengeGaugeSize(currentGaugeSize));
    }
    private void RemoveGaugeSize()
    {
        _innerGreen.fillAmount = 0;
    }
    private IEnumerator ChengeGaugeSize(float gaugeSize)
    {
        while (_innerGreen.fillAmount < gaugeSize && 
            gaugeSize - _innerGreen.fillAmount == CHENGE_END_VALUE)
        {
            _innerGreen.fillAmount = 
                Mathf.Lerp(_innerGreen.fillAmount, gaugeSize, GAUGE_SIZE_PROGRESS);
            yield return null;
        }
        _innerGreen.fillAmount = gaugeSize;
    }
}
