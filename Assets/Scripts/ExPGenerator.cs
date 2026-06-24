using UnityEngine;
using System;
public class ExPGenerator : MonoBehaviour
{
    //SmallExP用のイベント
    public event Func<GameObject> OnSmallExpSpawned;

    //LergeExP用のイベント
    public event Func<GameObject> OnLergeExPSpawned;

    //デスイベントを設定
    private void OnEnable()
    {
        DeathEventHub.OnDeath += GenerateExP;
    }
    private void OnDisable()
    {
        DeathEventHub.OnDeath -= GenerateExP;
    }
    private void GenerateExP(Vector3 position, int smallExPAmount, int lergeExPAmount)
    {
        for (int i = 0; i < smallExPAmount; i++)
        {
            GameObject b = null;

            //poolからオブジェクトを要求
            b = OnSmallExpSpawned?.Invoke();

            //座標を出現位置にセット
            b.transform.position = position;

            b.SetActive(true);
        }

        //LergeExP出現
        for (int i = 0; i < lergeExPAmount; i++)
        {
            GameObject b = null;

            //poolからオブジェクトを要求
            b = OnLergeExPSpawned?.Invoke();

            //座標を出現位置にセット
            b.transform.position = position;

            b.SetActive(true);
        }
    }
}
