using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Bullet : MonoBehaviour
{
    protected ProjectileMovementStrategy _movementStrategy;

    protected Rigidbody2D _rigidbody2D;
    protected Enemy _targetEnemy;
    protected float _bulletSpeed;
    protected float _damage;
    protected float _lifeTime;

    protected Vector2 _directionAfterEnemyDie;
    protected bool _isEnemyDied = false;

    protected void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0;
    }

    public virtual void Init(Enemy targetEnemy, float followSpeed, float lifeTime, float damage, ProjectileMovementStrategy movementStrategy)
    {
        _targetEnemy = targetEnemy;
        _bulletSpeed = followSpeed;
        _damage = damage;
        _movementStrategy = movementStrategy;
        _lifeTime = lifeTime;

        _directionAfterEnemyDie = transform.position - _targetEnemy.transform.position;
    }
}
