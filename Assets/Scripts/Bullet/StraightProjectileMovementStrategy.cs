using UnityEngine;

public class StraightProjectileMovementStrategy : ProjectileMovementStrategy
{
    override public Vector3 CalculateMovement(Transform projectilePosition, Vector3 target, float projectileSpeed)
    {
        Vector2 targetDirection = target - projectilePosition.position;
        targetDirection.Normalize();
        return (targetDirection * projectileSpeed * Time.fixedDeltaTime);
    }

    public override Vector3 CalculateMovementWithoutTargetEnemy(Transform projectilePosition, Vector3 target, float projectileSpeed)
    {
        target.Normalize();
        return (target * projectileSpeed * Time.fixedDeltaTime);
    }

    override public float CalculateRotation(Transform projectilePosition, Vector3 target)
    {
        Vector2 targetDirection = target - projectilePosition.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        return angle;
    }
}
