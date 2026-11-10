using UnityEngine;
using System;
public class PlayerInput : MonoBehaviour
{
    //弾発射イベント
    public event Action OnStartShot;
    public event Action OnEndShot;


    void Update()
    {
        InputShoot();
    }
    private void InputShoot()
    {
        //Bullet発射処理
        if (Input.GetButtonDown("Fire1"))
        {
            OnStartShot?.Invoke();
        }

        //発射状態脱出
        if (Input.GetButtonUp("Fire1"))
        {
            OnEndShot?.Invoke();
        }
    }
}
