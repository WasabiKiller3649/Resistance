using UnityEngine;

public class Tester : MonoBehaviour, IDamageable
{
    public void TakeDamage(float damage)
    {
        Debug.Log("TestDamage" + damage);
    }
}