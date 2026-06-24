using UnityEngine;

public static class AngleCalculator
{
    public static Quaternion GetRotationToTarget(Vector3 origine, Vector3 target, float offsetAngle = 90f)
    {
        Quaternion q = default;

        //ベクトルを取得（元いる座標-向きたい座標）
        Vector2 direction = target - origine;

        //回転取得
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        angle += offsetAngle;

        //変換
        q = Quaternion.Euler(0f, 0f, angle);
        return q;
    }
    public static float GetAngleToTarget(Vector3 origine, Vector3 target)
    {
        //ベクトルを取得（元いる座標-向きたい座標）
        Vector2 direction = target - origine;

        //回転取得
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        return angle;
    }
}
