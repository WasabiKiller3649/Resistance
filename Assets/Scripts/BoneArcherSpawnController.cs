using UnityEngine;
public class BoneArcherSpawnController : MonoBehaviour
{
    //ゲームマネージャー
    [SerializeField]
    private GameManager _gameManager;

    //ArcherGenerator
    [SerializeField]
    private BoneArcherGenerator _boneArcherGenerator;
    private void OnEnable()
    {
        _gameManager.OnBeginBoneArcherSpawn += BeginGenerating;
        _gameManager.OnBeginSpawnPaceIncrement += IncreaseSpawn;
        _gameManager.OnBeginSecondCasterSpawn += StopGenerate;
    }
    private void OnDisable()
    {
        _gameManager.OnBeginBoneArcherSpawn -= BeginGenerating;
        _gameManager.OnBeginSpawnPaceIncrement -= IncreaseSpawn;
        _gameManager.OnBeginSecondCasterSpawn -= StopGenerate;
    }

    private void BeginGenerating()//Archerスポーン開始
    {
        //ステージフェーズ進行により，骨の生成開始
        _boneArcherGenerator.StartGenerating();
        //念のため購読解除
        _gameManager.OnBeginBoneArcherSpawn -= BeginGenerating;
    }
    private void IncreaseSpawn()
    {
        _boneArcherGenerator.IncreaseSpawnRate();
    }
    private void StopGenerate()
    {
        _boneArcherGenerator.StopGenerate();
    }
}
