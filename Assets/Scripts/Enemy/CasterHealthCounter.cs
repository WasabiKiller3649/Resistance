using UnityEngine;
using System;
public class CasterHealthCounter : MonoBehaviour, IDamageable
{
    public event Action<float> OnTakeDamage;
    public event Action OnPlayHitAnimation;
    public void TakeDamage(float damage)
    {
        OnTakeDamage(damage);
        OnPlayHitAnimation();
    }
}
