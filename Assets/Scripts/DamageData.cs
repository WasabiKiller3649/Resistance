using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/DamageDate")]
public class DamageData : ScriptableObject
{
    //—^‚¦‚éƒ_ƒ[ƒW—Ê
    [SerializeField]
    private float _damage;
    public float GetDamage()
    {
        return _damage;
    }
}
