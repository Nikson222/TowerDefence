using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Towers;


public abstract class ShootingTower : Tower
{
    [Header("Tower Main Settings")]
    [SerializeField] protected float _damage;
    [SerializeField] protected float _delayShooting;
    [SerializeField] protected float _attackRadius;


    [Header("Bullet settings")]
    [SerializeField] protected Bullet _projectilePrefab;
    [SerializeField] protected Transform _shootPosition;
    [SerializeField] protected float _projectileSpeed;
    [SerializeField] protected float _projectileLifeTime;

    [SerializeField] protected float _enemyDetectingDelay;
    protected Enemy _targetEnemy;
    protected float _timeAfterDetect;

    protected bool _isShootAllow = true;

    public float Damage => _damage;
    public float DelayShooting => _delayShooting;

    protected override void Start()
    {
        base.Start();
        BulletPooler.Instance.SpawnPool(_projectilePrefab, this);
    }

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

    protected virtual void Shoot()
    {
        _isShootAllow = false;

        
        var currentBullet = BulletPooler.Instance.GiveBullet(this);

        currentBullet.transform.SetParent(null);
        currentBullet.enabled = true;
        currentBullet.transform.position = _shootPosition.position;
        currentBullet.transform.rotation = _shootPosition.rotation;

        currentBullet.Init(_targetEnemy, _projectileSpeed, _projectileLifeTime, _damage);

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

    virtual protected IEnumerator ShotRecovery()
    {
        yield return new WaitForSeconds(_delayShooting);
        _isShootAllow = true;
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

