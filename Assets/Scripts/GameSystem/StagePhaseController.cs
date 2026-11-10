using UnityEngine;

public class StagePhaseController : MonoBehaviour
{
    //ゲームマネージャー
    [SerializeField]
    private GameManager _manager;
    private void Start()
    {
       　//イベント登録
    }

    public void ChangeStagePhase()//状況に応じてフェーズを変える
    {
        switch (_manager.GetStagePhase())
        {
            case GameManager.StagePhase.Initial:
                _manager.SetStagePhase(GameManager.StagePhase.BoneArcherSpawn);
                break;
            case GameManager.StagePhase.BoneArcherSpawn:
                _manager.SetStagePhase(GameManager.StagePhase.EnemySpawnPaceIncrement);
                break;
            case GameManager.StagePhase.EnemySpawnPaceIncrement:
                _manager.SetStagePhase(GameManager.StagePhase.CasterSpawn);
                break;
            case GameManager.StagePhase.CasterSpawn:
                _manager.SetStagePhase(GameManager.StagePhase.SecondCasterSpawn);
                break;
        }
    }
}
