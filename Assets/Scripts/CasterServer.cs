using UnityEngine;

public class CasterServer : MonoBehaviour
{
    //キャスター一人目
    [SerializeField]
    private GameObject _firstCaster;

    //キャスター二人目
    [SerializeField]
    private GameObject _secondCaster;

    //GameManager
    [SerializeField]
    private GameManager _gameManager;
    private void OnEnable()
    {
        //一人出現
        _gameManager.OnBeginCasterSpawn += WakeFirstCaster;

        //二人出現
        _gameManager.OnBeginSecondCasterSpawn += WakeFirstCaster;
        _gameManager.OnBeginSecondCasterSpawn += WakeSecondCaster;

        //ゲームクリア時，
    }
    private void WakeFirstCaster()//一人目
    {
        _firstCaster.SetActive(true);
    }

    private void WakeSecondCaster()
    {
        _secondCaster.SetActive(true);//二人目
    }
    private void DownCaster()
    {
        _firstCaster.SetActive(false);
        _secondCaster.SetActive(false);
    }
}
