using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private ProjectileMovementStrategy _movementStrategy;
    private Rigidbody2D _rigidbody2D;
    private Enemy _targetEnemy;
    private float _bulletSpeed;
    private float _damage;
    private float _lifeTime;

    private Vector2 _directionAfterEnemyDie;
    private bool _isEnemyDied = false;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0;
    }

    private void FixedUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        if (_lifeTime <= 0)
        {
            Destroy(gameObject);
        }

        if (_targetEnemy.isActiveAndEnabled && !_isEnemyDied)
        {
            _directionAfterEnemyDie = transform.position - _targetEnemy.transform.position;
            _rigidbody2D.position += (Vector2)_movementStrategy.CalculateMovement(gameObject.transform, _targetEnemy.transform.position, _bulletSpeed);
            _rigidbody2D.MoveRotation(_movementStrategy.CalculateRotation(gameObject.transform, _targetEnemy.transform.position));
        }
        else
        {
            _isEnemyDied = true;
            _lifeTime -= Time.deltaTime;
            _rigidbody2D.position += (Vector2)_movementStrategy.CalculateMovementWithoutTargetEnemy(gameObject.transform, -_directionAfterEnemyDie, _bulletSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _targetEnemy.gameObject)
        {
            _targetEnemy.GetDamage(_damage);
            Destroy(gameObject);
        }
    }

    public void Init(Enemy targetEnemy, float followSpeed, float lifeTime, float damage, ProjectileMovementStrategy movementStrategy)
    {
        _targetEnemy = targetEnemy;
        _bulletSpeed = followSpeed;
        _damage = damage;
        _movementStrategy = movementStrategy;
        _lifeTime = lifeTime;

        _directionAfterEnemyDie = transform.position - _targetEnemy.transform.position;
    }
}
