using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveableEnemy : Enemy
{
    private Rigidbody2D _enemyRigidbody;

    [SerializeField] protected WayPoint[] _wayPointsList;
    protected int _wayPointIndex;
    protected Vector2 _CurrentWayPoint;

    protected void Start()
    {
        _enemyRigidbody = GetComponent<Rigidbody2D>();

        _wayPointIndex = 0;
        _CurrentWayPoint = _wayPointsList[_wayPointIndex].WayPointPosition;
    }
    private void Update()
    {
        if (CurrentWayPointReached())
            WayPointItterate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void InitPath(WayPoint[] wayPoints)
    {
        _wayPointsList = wayPoints;
        _wayPointIndex = 0;
        _CurrentWayPoint = _wayPointsList[_wayPointIndex].WayPointPosition;
    }

    protected bool CurrentWayPointReached()
    {
        float distanceToWaPoint = ((Vector2)transform.position - _CurrentWayPoint).magnitude;

        if (distanceToWaPoint < 0.1f) return true;
        else return false;
    }

    protected void WayPointItterate()
    {
        _wayPointIndex++;

        if (_wayPointIndex > _wayPointsList.Length - 1)
            gameObject.SetActive(false);
        else
            _CurrentWayPoint = _wayPointsList[_wayPointIndex].WayPointPosition;
    }

    public override void GetDamage(float damage)
    {
        _healthPoints -= damage;
        if (_healthPoints <= 0)
            Die();
    }

    protected override void Die()
    {
        gameObject.SetActive(false);
        _healthPoints = _maxHealthPoints;
    }

    protected override void Move()
    {
        _enemyRigidbody.position = Vector2.MoveTowards(transform.position, _CurrentWayPoint, _speed * Time.fixedDeltaTime);
    }
}
