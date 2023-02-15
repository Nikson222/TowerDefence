using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemies Wave", order = 2, fileName = "New Enemy Wave")]
public class ElementOfWaveSettings : ScriptableObject
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] float _spawnDelay;
    [SerializeField] int _countOfEnemy;
    [SerializeField] private float _switchToNextElementDelay;

    public GameObject EnemyPrefab => _enemyPrefab; 
    public float spawnDelay => _spawnDelay;
    public int countOfEnemy => _countOfEnemy;
    public float SwitchToNextElementDelay => _switchToNextElementDelay;
}
