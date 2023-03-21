using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTower : ShootingTower
{
    [SerializeField] protected float _rotationSpeed;
    [SerializeField] protected float _TargetViewingFault;


    protected virtual void FixedUpdate()
    {
        if (_targetEnemy && _targetEnemy.isActiveAndEnabled)
            FollowRotate();
    }

    protected virtual void FollowRotate()
    {
        Vector2 direction = _targetEnemy.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, angle), _rotationSpeed * Time.deltaTime);
    }

    protected virtual bool IsTowerLooksTarget()
    {
        Vector2 direction = _targetEnemy.transform.position - transform.position;
        direction.Normalize();
        float angle = Vector2.SignedAngle(transform.up, direction);
        return Mathf.Abs(angle) < _TargetViewingFault;
    }

    protected override bool _isAdditionalShootingÑonditionsTrue()
    {
        return IsTowerLooksTarget();
    }
}
