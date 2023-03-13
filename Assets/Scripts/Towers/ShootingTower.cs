using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Towers;
using ProjectileStrategies;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class ShootingTower : Tower
{
    protected Rigidbody2D _rigidBody2D;

    [Header("Tower Main Settings")]
    [SerializeField] protected float _damage;
    [SerializeField] protected float _delayShooting;
    [SerializeField] protected float _attackRadius;
    [SerializeField] protected float _rotationSpeed;
    [SerializeField] protected float _TargetViewingFault;
    [SerializeField] protected ProjectileStrategy _movementStrategy;


    [Header("Bullet settings")]
    [SerializeField] protected Bullet _bulletPrefab;
    [SerializeField] protected Transform _shootPosition;
    [SerializeField] protected float _bulletSpeed;
    [SerializeField] protected float _bulletLifeTime;

    protected Enemy _targetEnemy;
    [SerializeField] protected float _enemyDetectingDelay;
    protected float _timeAfterDetect;

    public List<UpgradeConfig> upgrades = new List<UpgradeConfig>();

    protected bool _isShootAllow = true;

    public float Damage => _damage;
    public float DelayShooting => _delayShooting;


    virtual protected void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _rigidBody2D.gravityScale = 0;
    }

    private void Update()
    {
        if (_targetEnemy)
        {
            if (IsTargetEnemyInRadius() && _targetEnemy.isActiveAndEnabled)
            {
                if (_isShootAllow && IsTowerLooksTarget())
                {
                    Shot();
                }
            }
            else
            {
                _targetEnemy = null;
            }
        }
        else
        {
            _timeAfterDetect -= Time.deltaTime;
            if (_timeAfterDetect <= 0)
            {
                DetectEnemy();
                _timeAfterDetect = _enemyDetectingDelay;
            }
        }
    }

    virtual protected void FixedUpdate()
    {
        if (_targetEnemy && _targetEnemy.isActiveAndEnabled)
            FollowRotate();
    }

    virtual protected void FollowRotate()
    {
        Vector2 direction = _targetEnemy.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        float newAngle = Mathf.MoveTowardsAngle(_rigidBody2D.rotation, angle, _rotationSpeed * Time.deltaTime);
        _rigidBody2D.MoveRotation(newAngle);
    }

    virtual protected void Shot()
    {
        _isShootAllow = false;

        Bullet bulletGameObject = Instantiate(_bulletPrefab);
        bulletGameObject.transform.position = _shootPosition.position;
        bulletGameObject.transform.rotation = _shootPosition.rotation;

        bulletGameObject.Init(_targetEnemy, _bulletSpeed, _bulletLifeTime, _damage, ProjectileStategiesStore.ProjectileStrateries[_movementStrategy]);

        StartCoroutine(ShotRecovery());
    }

    virtual protected bool IsTargetEnemyInRadius()
    {
        return Vector2.Distance(transform.position, _targetEnemy.transform.position) <= _attackRadius;
    }

    virtual protected void DetectEnemy()
    {
        var detectedEnemy = Physics2D.OverlapCircleAll(transform.position, _attackRadius);

        if (detectedEnemy != null)
        {
            detectedEnemy[detectedEnemy.Length - 1].TryGetComponent(out _targetEnemy);
        }
    }

    virtual protected bool IsTowerLooksTarget()
    {
        Vector2 direction = _targetEnemy.transform.position - transform.position;
        direction.Normalize();
        float angle = Vector2.SignedAngle(transform.up, direction);
        return Mathf.Abs(angle) < _TargetViewingFault;
    }

    virtual protected void SetMovementStrategy(ProjectileStrategy movementStrategy)
    {
        _movementStrategy = movementStrategy;
    }

    virtual protected IEnumerator ShotRecovery()
    {
        yield return new WaitForSeconds(_delayShooting);
        _isShootAllow = true;
    }

    [Serializable]
    public struct UpgradeConfig
    {
        public int level;
        public int newUpgradeCost;
        public Sprite newSprite;
        public int newDamage;
        public int newRange;
        public ProjectileStrategy ProjectileStrategy;
    }

    #region Editor
#if UNITY_EDITOR
    virtual protected void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 1, 0.2f);
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
#endif
    #endregion
}
