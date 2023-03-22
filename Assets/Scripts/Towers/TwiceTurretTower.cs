using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectileStrategies;

public class TwiceTurretTower : TurretTower
{
    [SerializeField] private Transform _firstRocketPosition;
    [SerializeField] private Transform _secondRocketPosition;
    [SerializeField] private Bullet _firstRocketBullet;
    [SerializeField] private Bullet _secondRocketBullet;
    private bool _usedSecondBulletPosition = false;

    protected override void Start()
    {
        base.Start();
    }


    protected override void Shoot()
    {

        _isShootAllow = false;

        if (_usedSecondBulletPosition)
        {
            EnableBullet(_secondRocketBullet);
            _secondRocketBullet = null;
        }
        else
        {
            EnableBullet(_firstRocketBullet);
            _firstRocketBullet = null;
        }

        StartCoroutine(ShotRecovery());
    }


    protected override IEnumerator ShotRecovery()
    {
        if (_usedSecondBulletPosition)
        {
            _shootPosition = _firstRocketPosition;
            _usedSecondBulletPosition = false;
        }
        else
        {
            _shootPosition = _secondRocketPosition;
            _usedSecondBulletPosition = true;
        }

        return base.ShotRecovery();
    }
    protected override IEnumerator BulletReload()
    {
        yield return new WaitForSeconds(DelayShooting / 1.5f);

        if (_firstRocketBullet == null)
        {
            _firstRocketBullet = BulletPooler.Instance.GiveBullet(this);
            DisableBullet(_firstRocketBullet, _firstRocketPosition);
        }

        if (_secondRocketBullet == null)
        {
            _secondRocketBullet = BulletPooler.Instance.GiveBullet(this);
            DisableBullet(_secondRocketBullet, _secondRocketPosition);
        }
    }


    private void EnableBullet(Bullet bullet)
    {
        if (bullet == null)
        {
            StartCoroutine(BulletReload());
            return;
        }

        bullet.transform.SetParent(null);
        bullet.enabled = true;

        bullet.Init(_targetEnemy, _projectileSpeed, _projectileLifeTime, _damage, ProjectileStategiesStore.ProjectileStrateries[_movementStrategy]);
    }

    private void DisableBullet(Bullet bullet, Transform rocketPosition)
    {
        bullet.transform.SetParent(transform);

        bullet.transform.position = rocketPosition.position;
        bullet.transform.rotation = rocketPosition.rotation;

        bullet.enabled = false;
    }
}
