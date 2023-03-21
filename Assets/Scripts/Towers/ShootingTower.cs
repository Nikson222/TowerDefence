using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Towers;
using ProjectileStrategies;


public abstract class ShootingTower : Tower
{
    [Header("Tower Main Settings")]
    [SerializeField] protected float _damage;
    [SerializeField] protected float _delayShooting;
    [SerializeField] protected float _attackRadius;

    [SerializeField] protected ProjectileStrategy _movementStrategy;


    [Header("Bullet settings")]
    [SerializeField] protected Bullet _projectilePrefab;
    [SerializeField] protected Transform _shootPosition;
    [SerializeField] protected float _projectileSpeed;
    [SerializeField] protected float _projectileLifeTime;

    [SerializeField] protected float _enemyDetectingDelay;
    protected Enemy _targetEnemy;
    protected float _timeAfterDetect;

    public List<UpgradeConfig> Upgrades = new List<UpgradeConfig>();

    protected bool _isShootAllow = true;

    public float Damage => _damage;
    public float DelayShooting => _delayShooting;

    protected virtual void Update()
    {
        if (_targetEnemy)
        {
            if (_isTargetEnemyInRadius() && _targetEnemy.isActiveAndEnabled)
            {
                if (_isShootAllow && _isAdditionalShooting—onditionsTrue())
                {
                    Shoot();
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

    protected virtual bool _isAdditionalShooting—onditionsTrue()
    {
        return true;
    }

    virtual protected void Shoot()
    {
        _isShootAllow = false;

        Bullet bulletGameObject = Instantiate(_projectilePrefab);
        bulletGameObject.transform.position = _shootPosition.position;
        bulletGameObject.transform.rotation = _shootPosition.rotation;

        bulletGameObject.Init(_targetEnemy, _projectileSpeed, _projectileLifeTime, _damage, ProjectileStategiesStore.ProjectileStrateries[_movementStrategy]);

        StartCoroutine(ShotRecovery());
    }

    virtual protected bool _isTargetEnemyInRadius()
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

    virtual protected void SetProjectileMovementStrategy(ProjectileStrategy movementStrategy)
    {
        _movementStrategy = movementStrategy;
    }

    virtual protected IEnumerator ShotRecovery()
    {
        yield return new WaitForSeconds(_delayShooting);
        _isShootAllow = true;
    }

    public override void UpgradeTower()
    {
        if (Upgrades.Count > _towerLevel)
            _updgadePrice = Upgrades[_towerLevel].newUpgradeCost;
        _damage = Upgrades[_towerLevel].newDamage;
        _attackRadius = Upgrades[_towerLevel].newRange;
        SetProjectileMovementStrategy(Upgrades[_towerLevel].ProjectileStrategy);

        if (Upgrades[_towerLevel].newSprite != null)
            GetComponent<SpriteRenderer>().sprite = Upgrades[_towerLevel].newSprite;

        _towerLevel++;
    }

    virtual protected void SetMovementStrategy(ProjectileStrategy movementStrategy)
    {
        _movementStrategy = movementStrategy;
    }

    [Serializable]
    public struct UpgradeConfig
    {
        public float level;
        public int newUpgradeCost;
        public Sprite newSprite;
        public float newDamage;
        public float newRange;
        public float newRotateSpeed;
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

