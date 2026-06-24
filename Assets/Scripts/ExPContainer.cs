using UnityEngine;
using System;
public class ExPContainer : MonoBehaviour
{
    //経験値を数値で格納
    private float _experiencePoint = default;

    //初期のレベルアップ値
    [SerializeField]
    private int _initialExPRequired;

    //この値になるとレベルアップする
    private float _exPRequired;

    //レベルアップに必要な経験値量はこの倍率で増える
    private const float EXP_GROWTH_RATE = 1.5f;

    //ゲージの大きさを変えるイベント
    public event Action<float, float> OnUpdateExP;

    //レベルアップイベント
    public event Action OnNextLevel;

    private void Awake()
    {
        //レベルアップ値初期化
        _exPRequired = _initialExPRequired;
    }
    public void AddExPValue(float value)
    {
        _experiencePoint += value;

        //ExPコンテナの最大値と現在値を渡す
        OnUpdateExP(_exPRequired, _experiencePoint);
        //レベルアップするかどうか
        CheckLevel();
    }
    private void RemoveExPValue(float value)
    {
        _experiencePoint -= value;
    }
    private void CheckLevel()
    {
        //現在のExP量がレベルアップ必要量を超えたら
        if (_experiencePoint >= _exPRequired)
        {
            //レベルアップに必要な量を減らす
            RemoveExPValue(_exPRequired);

            //必要経験値量引き上げ
            _exPRequired = Mathf.RoundToInt(_exPRequired * EXP_GROWTH_RATE);

            //レベルアップ
            OnNextLevel();
        }
    }
}
