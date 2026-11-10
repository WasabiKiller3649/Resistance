using UnityEngine;
using System.Collections;
public class LevelUPTimeController : MonoBehaviour
{
    //LevelUpイベント参照先
    [SerializeField]
    private ExPContainer _exPContainer;

    //時間が止まるまでの猶予
    [SerializeField]
    private float _timeStopDuration;

    //timeScaleがこれ以下になったら時間停止とみなす
    private const float STOP_TIME_SCALE = 0.1f;

    //コルーチンを止める用
    private Coroutine _timeStopCoroutine;
    private void OnEnable()
    {
        //時間停止イベントを入れる
        _exPContainer.OnNextLevel += StopTime;
    }
    private void StopTime()
    {
        //時間を緩やかに止める
        _timeStopCoroutine = StartCoroutine(GraduallyTimeStop());
    }
    private IEnumerator GraduallyTimeStop()
    {
        while (Time.timeScale >= STOP_TIME_SCALE)
        {
            //Time.timeScaleを徐々に0に近づける
            Time.timeScale -= Time.deltaTime * _timeStopDuration;
            yield return null;
        }

        //時間を完全停止させる
        Time.timeScale = 0;
    }
    public void RestartTime()
    {
        //時間停止コルーチンを止める
        StopCoroutine(_timeStopCoroutine);
        Time.timeScale = 1;
    }
}
