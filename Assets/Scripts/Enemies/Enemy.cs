using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float _healthPoints;
    [SerializeField] protected float _maxHealthPoints;
    [SerializeField] protected float _speed;

    abstract protected void Move();
    abstract protected void GetDamage();
    abstract protected void Die();
}
