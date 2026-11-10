using UnityEngine;

public class EnemyDamageServer : MonoBehaviour
{
    //ダメージデータ
    [SerializeField]
    private DamageData _damageDate;

    //与えるダメージ量
    private float _damageValue;
    //与えるダメ―ジ量取得
    private void OnEnable()
    {
        _damageValue = _damageDate.GetDamage();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(_damageValue);
        }
    }
}
