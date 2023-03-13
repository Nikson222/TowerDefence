using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStore : MonoBehaviour
{
    [SerializeField] private GameObject[] _towerPrefabs;

    public GameObject[] TowerPrefabs => _towerPrefabs;
}
