using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LastBossController _lastBossController;
    //骨Archer出現イベント
    public event Action OnBeginBoneArcherSpawn;

    //スポーン加速イベント
    public event Action OnBeginSpawnPaceIncrement;

    //キャスター出現一回目
    public event Action OnBeginCasterSpawn;

    //キャスター出現二回目
    public event Action OnBeginSecondCasterSpawn;

    //最終ボス出現
    public event Action OnBeginLastBoss;

    //ゲームクリアイベント
    public event Action OnLastBossDead;
    public enum StagePhase
    {
        Initial,
        BoneArcherSpawn,
        EnemySpawnPaceIncrement,
        CasterSpawn,
        SecondCasterSpawn,
        LastBoss,
    }
    //ゲームのフェーズ
    private StagePhase stagePhase;
    private void Awake()
    {
        //フェーズの初期化
        stagePhase = StagePhase.Initial;
    }
    private void OnEnable()
    {
        _lastBossController.OnDeath += OnGameClear;
    }
    public void RaiseChangeStagePhaseEvent()//外部からイベントを起こすときのメソッド
    {
        switch (stagePhase)
        {
            case StagePhase.BoneArcherSpawn:
                print("骨出現");
                OnBeginBoneArcherSpawn?.Invoke();
                break;
            case StagePhase.EnemySpawnPaceIncrement:
                print("スポーン早まる");
                OnBeginSpawnPaceIncrement?.Invoke();
                break;
            case StagePhase.CasterSpawn:
                print("Caster一人出現");
                OnBeginCasterSpawn?.Invoke();
                break;
            case StagePhase.SecondCasterSpawn:
                print("Caster二人出現");
                OnBeginSecondCasterSpawn?.Invoke();
                break;
            case StagePhase.LastBoss:
                print("最終ボス出現");
                OnBeginLastBoss?.Invoke();
                break;
        }
    }
    private void OnGameClear()
    {
        OnLastBossDead?.Invoke();
    }
    public void SetStagePhase(StagePhase phase)
    {
        stagePhase = phase;
    }
    public StagePhase GetStagePhase()
    {
        return stagePhase;
    }
}
