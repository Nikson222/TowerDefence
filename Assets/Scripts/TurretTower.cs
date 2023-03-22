using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectileStrategies;

public class TurretTower : ShootingTower
{
    [SerializeField] protected float _rotationSpeed;
    [SerializeField] protected float _TargetViewingFault;

    protected Bullet _currentBullet;

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

    protected override IEnumerator ShotRecovery()
    {
        StartCoroutine(BulletReload());
        return base.ShotRecovery();
    }

    protected virtual IEnumerator BulletReload()
    {
        yield return new WaitForSeconds(DelayShooting/2);
        _currentBullet = BulletPooler.Instance.GiveBullet(this);

        _currentBullet.enabled = false;
        _currentBullet.transform.SetParent(transform);

        _currentBullet.transform.position = _shootPosition.position;
        _currentBullet.transform.rotation = _shootPosition.rotation;
    }

    protected override void Shoot()
    {
        _isShootAllow = false;

        if (_currentBullet == null)
            _currentBullet = BulletPooler.Instance.GiveBullet(this);

        _currentBullet.transform.SetParent(null);
        _currentBullet.enabled = true;
        _currentBullet.transform.position = _shootPosition.position;
        _currentBullet.transform.rotation = _shootPosition.rotation;

        _currentBullet.Init(_targetEnemy, _projectileSpeed, _projectileLifeTime, _damage, ProjectileStategiesStore.ProjectileStrateries[_movementStrategy]);

        StartCoroutine(ShotRecovery());
    }
}
