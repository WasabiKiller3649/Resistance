using UnityEngine;

public class LastBossStarter : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private GameObject _lastBoss;
    private void OnEnable()
    {
        _gameManager.OnBeginLastBoss += WakeUpBoss;
    }
    private void OnDisable()
    {
        _gameManager.OnBeginLastBoss -= WakeUpBoss;
    }
    private void WakeUpBoss()
    {
        _lastBoss.SetActive(true);
    }
}
