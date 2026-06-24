using UnityEngine;
using System;
public class UpdateHealthEventHub : MonoBehaviour
{
    public event Action<int, int> OnUpdateHealth;
    public void RaiseUpdateHealth(int maxValue, int currentValue)
    {
        OnUpdateHealth?.Invoke(maxValue, currentValue);
    }
}
