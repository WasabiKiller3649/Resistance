using UnityEngine;

public class LastBossAnimatorController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    public void StartRun()
    {
        _animator.SetBool("isRun", true);
    }
    public void EndRun()
    {
        _animator.SetBool("isRun", false);
    }
    public void Death()
    {
        _animator.SetBool("Dead", true);
    }
}
