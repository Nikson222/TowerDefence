using UnityEngine;

public abstract class ProjectileMovementStrategy
{
    abstract public Vector3 CalculateMovement(Transform projectilePosition, Vector3 target, float projectileSpeed);
    abstract public Vector3 CalculateMovementWithoutTargetEnemy(Transform projectilePosition, Vector3 target, float projectileSpeed);
    abstract public float CalculateRotation(Transform projectilePosition, Vector3 target);
}

