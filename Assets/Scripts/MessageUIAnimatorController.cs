using UnityEngine;

public class MessageUIAnimatorController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    //レベルアップイベント参照
    [SerializeField]
    private ExPContainer _exPContainer;
    private void OnEnable()
    {
        _exPContainer.OnNextLevel += PlayAppearanceAnimation;
    }
    private void PlayAppearanceAnimation()
    {
        _animator.Play("LevelUpMessageAppearanceAnimation");
    }
    public void PlayEjectionAnimation()
    {
        _animator.Play("LevelUpMessageEjectionAnimation");
    }
}
