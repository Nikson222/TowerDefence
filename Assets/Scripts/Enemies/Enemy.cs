using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private Action<int> OnDie;
    [SerializeField] protected float _healthPoints;
    [SerializeField] protected float _maxHealthPoints;
    [SerializeField] protected float _speed;
    [SerializeField] protected int _killPrize;

    protected virtual void Start()
    {
        OnDie += PlayerData.GetMoney;
    }

    abstract protected void Move();
    abstract public void GetDamage(float damage);
    virtual protected void Die()
    {
        OnDie?.Invoke(_killPrize);
    }
}
