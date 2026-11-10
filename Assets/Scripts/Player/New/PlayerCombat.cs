using UnityEngine;

public class PlayerCombat
{
    private readonly PlayerCombatData _combatData = default;

    private bool _canAttack = true;
    private float _elapsedTime = 0.0f;

    public PlayerCombat(PlayerCombatData combatData)
    {
        _combatData = combatData;
    }

    /// <summary>
    /// AttackIntervalの経過時間を更新する
    /// </summary>
    /// <param name="deltaTime"></param>
    public void UpdateAttackInterval(float deltaTime)
    {
        if (_canAttack)
        {
            return;
        }

        _elapsedTime += deltaTime;

        if(_elapsedTime >= _combatData.AttackInterval)
        {
            _elapsedTime = 0.0f;
            _canAttack = true;
        }
    }

    /// <summary>
    /// 最も近い敵に向かって攻撃をする，
    /// AttackInterval中は攻撃できない
    /// 毎フレーム呼び出す
    /// </summary>
    /// <param name="playerPos"></param>
    /// <param name="isAttackPressed"></param>
    /// <returns>何らかの理由で攻撃できない場合はFalseを返す</returns>
    public bool TryAttack(Vector3 playerPos, bool isAttackPressed)
    {
        if (!_canAttack)
        {
            return false;
        }
        
        if (isAttackPressed &&
            TryGetTarget(playerPos, out IDamageable target, out Vector2 targetPos))
        {
            float mouseAngle = AngleCalculator.GetAngleToTarget(playerPos, targetPos);

            Debug.Log(mouseAngle);
            _canAttack = false;

            return true;
        }

        return false;
    }

    private bool TryGetTarget(Vector2 center, out IDamageable target, out Vector2 targetPos)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, _combatData.AttackRadius);

        float minDistance = Mathf.Infinity;
        target = default;
        targetPos = default;
        for(int i = 0; i < colliders.Length; i++)
        {
            float distance = Vector2.Distance(colliders[i].transform.position, center);

            if (colliders[i].TryGetComponent(out IDamageable damageable) &&
                minDistance > distance)
            {
                target = damageable;
                targetPos = colliders[i].transform.position;
            }
        }

        if(target == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}