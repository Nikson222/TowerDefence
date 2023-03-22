using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class TowerBlueprint
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _upgradeCost;

    public GameObject Prefab { get { return _prefab; } }
    private int UpgradeCost { get { return _upgradeCost; } }
}
