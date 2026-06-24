using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [SerializeField]
    private PlayerController _playerController;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _shotSound;
    [SerializeField]
    private AudioClip _hitSound;
    private void OnEnable()
    {
        _playerController.SubscribeShotSE(PlayShotSound);
        _playerController.SubscribeHitSE(PlayHitSound);
    }
    private void PlayShotSound()//ŽËŒ‚SE
    {
        _audioSource.PlayOneShot(_shotSound);
    }
    private void PlayHitSound()
    {
        _audioSource.PlayOneShot(_hitSound);
    }
}
