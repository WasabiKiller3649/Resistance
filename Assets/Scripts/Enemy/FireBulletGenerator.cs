using UnityEngine;
using System;
using System.Collections;
public class FireBulletGenerator : MonoBehaviour
{
    public event Func<GameObject> OnRequestFireBullet;

    //Caster
    [SerializeField]
    private CasterController _caster;
    private void Awake()
    {
        _caster.OnShootBullet += GenerateBullet;
    }
    private GameObject GenerateBullet()
    {
        GameObject bullet = default;
        bullet = OnRequestFireBullet();
        return bullet;
    }
}
