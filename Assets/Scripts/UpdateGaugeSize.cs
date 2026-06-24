using UnityEngine;

public class UpdateGaugeSize
{
    //最大値と現在値から，割合を計算し，掛けた値を返す
    public float CalcuratePercentage(float gaugeSize, float maxValue, float currentValue)
    {
        float percentage = currentValue / maxValue;
        gaugeSize *= percentage;
        return gaugeSize;
    }
}
