using UnityEngine;

public class Recorder : MonoBehaviour
{
    //ゲームマネージャー
    [SerializeField]
    private GameManager _manager;

    //ステージフェーズを変える
    [SerializeField]
    private StagePhaseController _phaseController;
    //この時間になるとステージが一段階進む
    [SerializeField]
    private float _boneArcherSpawnTime;

    //第二段階へ進む時間
    [SerializeField]
    private float _enemySpawnPaceIncrementTime;

    //中ボスが出てくる
    [SerializeField]
    private float _casterSpawnTime;

    //中ボス出てくる二回目
    [SerializeField]
    private float _secondCasterSpawnTime;

    //最終ボス出現
    [SerializeField]
    private float _lastBossSpawnTime;
    
    //ゲームの経過時間を記録
    private float _elapsedTime = 0;

    private void Update()
    {
        //経過時間記録
        _elapsedTime += Time.deltaTime;
        switch (_manager.GetStagePhase())
        {
            case GameManager.StagePhase.Initial:
                //Archerが出現する
                ChangeStagePhase(_boneArcherSpawnTime, GameManager.StagePhase.BoneArcherSpawn);
                break;
            case GameManager.StagePhase.BoneArcherSpawn:
                //スポーン速度が上がる
                ChangeStagePhase(_enemySpawnPaceIncrementTime, GameManager.StagePhase.EnemySpawnPaceIncrement);
                break;
            case GameManager.StagePhase.EnemySpawnPaceIncrement:
                ChangeStagePhase(_casterSpawnTime, GameManager.StagePhase.CasterSpawn);
                break;
            case GameManager.StagePhase.CasterSpawn:
                ChangeStagePhase(_secondCasterSpawnTime, GameManager.StagePhase.SecondCasterSpawn);
                break;
            case GameManager.StagePhase.SecondCasterSpawn:
                ChangeStagePhase(_lastBossSpawnTime, GameManager.StagePhase.LastBoss);
                break;
        }
    }
    private void ChangeStagePhase(float nextPhaseTime, GameManager.StagePhase stagePhase)
    {
        //フェーズを変える時間になったら
        if (CheckCurrentPhase(nextPhaseTime))
        {
            //現在のステージフェーズを次のフェーズへ
            _manager.SetStagePhase(stagePhase);

            //フェーズが変わったことを通知
            _manager.RaiseChangeStagePhaseEvent();
        }
    }
    private bool CheckCurrentPhase(float nextPhaseTime)//フェーズを変える時間かチェック
    {
        if (_elapsedTime >= nextPhaseTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
