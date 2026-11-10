using UnityEngine;

public class PlayerDamageServer : MonoBehaviour
{
    //与えるダメージ量
    private DamageManager _manager;
    public void SetDamageManager(DamageManager manager)
    {
        _manager = manager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ダメージを与えられるかチェック/trueならif文突入
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_manager.GetCurrentDamageValue());
        }
    }
}
