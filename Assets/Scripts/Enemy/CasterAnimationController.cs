using UnityEngine;
using System.Collections;
public class CasterAnimationController : MonoBehaviour
{
    //ヒット時のイベント参照先
    [SerializeField]
    private CasterHealthCounter _counter;

    [SerializeField]
    private SpriteRenderer _sprite;
    private void OnEnable()
    {
        _counter.OnPlayHitAnimation += PlayHitAnimation;
    }
    private void PlayHitAnimation()
    {
        Color c = _sprite.color;
        c.a = 0;
        _sprite.color = c;

        if (gameObject.activeSelf)
        {
            StartCoroutine(tm());
        }
    }
    private IEnumerator tm()
    {
        yield return new WaitForSeconds(0.1f);
        EndHitAnimation();
    }
    private void EndHitAnimation()
    {
        Color c = _sprite.color;
        c.a = 1;
        _sprite.color = c;
    }
}
