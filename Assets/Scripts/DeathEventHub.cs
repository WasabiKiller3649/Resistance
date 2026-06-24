using UnityEngine;
using System;
public static class DeathEventHub
{
    //(ExP出現位置、SmallExP出現数、LergeExP出現数)
    public static event Action<Vector3, int, int> OnDeath;

    public static void RaiseDeath(Vector3 pos, int smallExPAmount, int lergeExPAmount)
    {
        //ExPを生成するイベント(座標, 小ExP数, デカExP数)
        OnDeath?.Invoke(pos, smallExPAmount, lergeExPAmount);
    }
}
