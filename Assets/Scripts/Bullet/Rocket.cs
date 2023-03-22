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
            CalculateMovement();
            CalculateRotation();
        }
        else
        {
            _lifeTime -= Time.deltaTime;
            CalculateMovementWithoutTargetEnemy(-_directionAfterEnemyDie);
        }
    }


    private void CalculateMovement()
    {
        Vector2 targetDirection = _targetEnemy.transform.position - transform.position;
        targetDirection.Normalize();
        _rigidbody2D.position += (targetDirection * _bulletSpeed * Time.fixedDeltaTime);
    }

    private void CalculateMovementWithoutTargetEnemy(Vector3 target)
    {
        target.Normalize();
        _rigidbody2D.position += (Vector2)(target * _bulletSpeed * Time.fixedDeltaTime);
    }

    private void CalculateRotation()
    {
        Vector2 targetDirection = _targetEnemy.transform.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        _rigidbody2D.MoveRotation(angle);
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
