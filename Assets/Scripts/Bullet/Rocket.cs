using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rocket : Bullet
{
    private void FixedUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        if (_lifeTime <= 0)
        {
            gameObject.SetActive(false);
        }

        if (_targetEnemy.isActiveAndEnabled)
        {
            _directionAfterEnemyDie = transform.position - _targetEnemy.transform.position;
            _rigidbody2D.position += (Vector2)_movementStrategy.CalculateMovement(gameObject.transform, _targetEnemy.transform.position, _bulletSpeed);
            _rigidbody2D.MoveRotation(_movementStrategy.CalculateRotation(gameObject.transform, _targetEnemy.transform.position));
        }
        else
        {
            _lifeTime -= Time.deltaTime;
            _rigidbody2D.position += (Vector2)_movementStrategy.CalculateMovementWithoutTargetEnemy(gameObject.transform, -_directionAfterEnemyDie, _bulletSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_targetEnemy && collision.gameObject == _targetEnemy.gameObject)
        {
            _targetEnemy.GetDamage(_damage);
            gameObject.SetActive(false);
        }
    }
}
