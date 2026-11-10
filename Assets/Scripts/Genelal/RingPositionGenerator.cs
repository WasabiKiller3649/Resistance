using UnityEngine;

public class RingPositionGenerator
{
    private const float FULL_ANGLE = 360f;
    public Vector2 GetRandomPosition(float minRadius, float maxRadius, Vector2 center)
    {
        float angle = Random.Range(0f, FULL_ANGLE);
        float radius = Random.Range(minRadius, maxRadius);

        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

        Vector2 pos = center + new Vector2(x, y);
        return pos;
    }
    //半径，角度，中心点の座標を使い，中心から角度方向へ半径分移動した座標が求まる
    public Vector2 GetFunPosition(float radius, float funAngle, Vector2 center)
    {
        float x = Mathf.Cos(funAngle * Mathf.Deg2Rad) * radius;
        float y = Mathf.Sin(funAngle * Mathf.Deg2Rad) * radius;

        Vector2 pos = center + new Vector2(x, y);
        return pos;
    }
}
