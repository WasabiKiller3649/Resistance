using UnityEngine;

public class HealthContainer
{
    //Player‚ÌHP
    private int _currentHealth = 100;

    //Œ¸‚èƒCƒxƒ“ƒg
    [SerializeField]
    private HPController _counter;
    public HealthContainer(int max)
    {
        _currentHealth = max;
    }
    public void TakeDamage(float damage)
    {
        _currentHealth+= (int)damage;
    }
    public void TakeHeal(int amount)
    {
        _currentHealth+= amount;
    }
    public int GetCurrentHealth()
    {
        return _currentHealth;
    }
}
