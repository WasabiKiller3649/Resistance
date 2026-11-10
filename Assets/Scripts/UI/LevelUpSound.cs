using UnityEngine;

public class LevelUpSound : MonoBehaviour
{
    [SerializeField]
    private ExPContainer _exPContainer;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _choiceSound;
    [SerializeField]
    private AudioClip _levelUpSound;
    private void OnEnable()
    {
        _exPContainer.OnNextLevel += PlayLevelUpSound;
    }
    private void OnDisable()
    {
        _exPContainer.OnNextLevel -= PlayLevelUpSound;
    }
    private void PlayLevelUpSound()
    {
        _audioSource.PlayOneShot(_levelUpSound);
    }
    public void PlayChoiceSound()
    {
        _audioSource.PlayOneShot(_choiceSound);
    }
}
