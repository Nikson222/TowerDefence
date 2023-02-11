using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveableEnemy
{
    [SerializeField] protected List<WayPoint> _wayPointsList { get; set; }
    protected int _wayPointIndex { get; set; }
    protected Vector2 _CurrentWayPoint { get; set; }

    protected void Move();
}
