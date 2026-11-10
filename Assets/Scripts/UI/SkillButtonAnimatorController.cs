using UnityEngine;

public class SkillButtonAnimatorController : MonoBehaviour
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
        _animator.Play("SkillButtonAppearance");
    }
    public void PlayEjectionAnimation()
    {
        _animator.Play("SkillButtonEjection");
    }
}
