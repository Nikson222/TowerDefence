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

    public void InitPath(WayPoint[] wayPoints)
    {
        _wayPointsList = wayPoints;
    }

    protected void Start()
    {
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

    protected bool CurrentWayPointReached()
    {
        float distanceToWaPoint = ((Vector2)transform.position - _CurrentWayPoint).magnitude;

        if (distanceToWaPoint < 0.1f)
            return true;
        else
            return false;
    }

    protected void WayPointItterate()
    {
        _wayPointIndex++;

        if (_wayPointIndex > _wayPointsList.Length - 1)
            Destroy(gameObject);
        else
            _CurrentWayPoint = _wayPointsList[_wayPointIndex].WayPointPosition;
    }

    protected override void GetDamage()
    {
        throw new System.NotImplementedException();
    }

    protected override void Die()
    {
        throw new System.NotImplementedException();
    }

    protected override void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, _CurrentWayPoint, _speed * Time.fixedDeltaTime);
    }
}
