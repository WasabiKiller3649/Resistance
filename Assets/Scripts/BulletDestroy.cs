using UnityEngine;
using System.Collections;
public class BulletDestroy : MonoBehaviour
{
    //Á‚¦‚é‚Ü‚Å‚ÌŠÔ
    [SerializeField]
    private float _appearanceTime;

    private WaitForSeconds _waitDestroy;
    private void Awake()
    {
        _waitDestroy = new WaitForSeconds(_appearanceTime);
    }
    private void OnEnable()
    {
        StartCoroutine(DereyDestroy());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //æ‚É‘Šè‚ªHP‚¿‚©ŒŸ¸
        if (!collision.TryGetComponent(out IDamageable damageable))
        {
            if (collision.TryGetComponent(out IBreakable breakable))
            {
                breakable.Destroy();
            }
        }
        gameObject.SetActive(false);
    }
    private IEnumerator DereyDestroy()
    {
        yield return _waitDestroy;
        gameObject.SetActive(false);
    }
}
