using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float _healthPoints;
    [SerializeField] protected float _maxHealthPoints;
    [SerializeField] protected float _speed;

    abstract protected void Move();
    abstract public void GetDamage(float damage);
    abstract protected void Die();
}
