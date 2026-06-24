using UnityEngine;

public class GetRandomFanAngle : MonoBehaviour
{
    //イベント参照先
    [SerializeField]
    private CasterController _caster;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _caster.OnRequestBulletRandomAngle += GetAngle;
    }
    private Quaternion GetAngle(float maxAngle)
    {
        // 扇状角度
        float angle = Random.Range(-maxAngle/2f, maxAngle / 2f);

        //Unityで計算しやすいラジアンに変換する
        float rad = angle * Mathf.Deg2Rad;

        // ランダム座標
        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

        //Caster基準のローカル座標に変換
        Vector2 targetPos = (Vector2)transform.position + dir * 5f;


        return AngleCalculator.GetRotationToTarget(transform.position, targetPos, 0);
    }
}
